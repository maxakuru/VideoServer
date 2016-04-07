using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE3314Assignment1
{
    class RTPpacket
    {
        byte[] header;
        byte[] payload;
        byte[] packet;
        private static int interval = 100;
        private byte[] someArray;
        private int seqNo;
        private int length;

        public RTPpacket(byte[] data, int seqNo)
        {
            int timestamp = seqNo * interval;
            header = createHeader(seqNo, timestamp);
            payload = new byte[data.Length];
            payload = data;
            packet = new byte[data.Length + 12];
            for (int i = 0; i < 12; i++)
            {
                packet[i] = header[i];
            }
            for (int j = 12; j < data.Length + 12; j++)
            {
                packet[j] = payload[j - 12];
            }
        }

        public RTPpacket(byte[] data, int seqNo, int length)
        {
            // TODO: Complete member initialization
            this.someArray = data;
            this.seqNo = seqNo;
            this.length = length;

            int timestamp = seqNo * interval;
            header = createHeader(seqNo, timestamp);
            payload = new byte[length];
            payload = data;
            packet = new byte[length + 12];
            for (int i = 0; i < 12; i++)
            {
                packet[i] = header[i];
            }
            for (int j = 12; j < length + 12; j++)
            {
                packet[j] = payload[j-12];
            }
        }
        public byte[] createHeader(int sequenceNum, int tStamp)
        {
            int version = 2;
            int padding = 0;
            int extension = 0;
            int csrcCount = 0;
            int marker = 0;
            int payloadType = 26;
            int sequenceNumber = sequenceNum;
            long timestamp = tStamp;
            long SSRC = 0;

            byte[] buf = new byte[12]; //allocate this big enough to hold the RTP header + audio data

            //assemble the first bytes according to the RTP spec 

            buf[0] = (byte)((version & 0x3) << 6 | (padding & 0x1) << 5 | (extension & 0x0) << 4 | (csrcCount & 0x0));

            //2.byte
            buf[1] = (byte)((marker & 0x1) << 7 | payloadType & 0x7f);

            //squence number, 2 bytes, in big endian format. So the MSB first, then the LSB.
            buf[2] = (byte)((sequenceNumber & 0xff00) >> 8);
            buf[3] = (byte)(sequenceNumber & 0x00ff);

            //packet timestamp , 4 bytes in big endian format
            buf[4] = (byte)((timestamp & 0xff000000) >> 24);
            buf[5] = (byte)((timestamp & 0x00ff0000) >> 16);
            buf[6] = (byte)((timestamp & 0x0000ff00) >> 8);
            buf[7] = (byte)(timestamp & 0x000000ff);

            //our CSRC , 4 bytes in big endian format
            buf[8] = (byte)((SSRC & 0xff000000) >> 24);
            buf[9] = (byte)((SSRC & 0x00ff0000) >> 16);
            buf[10] = (byte)((SSRC & 0x0000ff00) >> 8);
            buf[11] = (byte)(SSRC & 0x000000ff);


            return buf;
        }

        public byte[] getBytes()
        {
            return packet;
        }

        internal string getHeader()
        {
            var result = string.Concat(header.Select(b => Convert.ToString(b, 2).PadLeft(8, '0').PadRight(9,' ')));
            return result;
        }
    }

    /*
    class RTPpacket
    {
        private byte[] header;
        private byte[] frame;
        private int version, padding, extension, cc, marker, ssrc, seqNo, timeStamp, payloadType, firstPiece;
        private static int rate = 80;
        private static int HEADERSIZE = 12;

        public RTPpacket()
        {

        }

        public RTPpacket(byte[] p, int seqNo)
        {
            version = 2;
            version = version << 6;
            padding = 1;
            padding = padding << 5;
            firstPiece = version | padding;
            extension = 0;
            cc = 0;
            marker = 0;
            ssrc = 0;

            //fill changing header fields:
            this.seqNo = seqNo;
            timeStamp = seqNo*rate;
            payloadType = 26;

            frame = makeFrame(p);
        }

        private byte[] makeFrame(byte[] basePacket)
        {
            //make 12 header bytes
            header = new byte[HEADERSIZE];
            header[0] = (byte)firstPiece;
            header[1] = (byte)payloadType;
            header[2] = (byte)seqNo;
            header[4] = (byte)timeStamp;
            header[5] = 0;
            header[6] = 0;
            header[7] = 0;
            header[8] = 0;
            header[9] = 0;
            header[10] = 0;
            header[11] = 0;
            Console.Write("header as string: " + header.ToString());

            //check length of basePacket
            int length = basePacket.Length;

            //create new byte array of size length + 12 bytes
            byte[] fullFrame = new byte[length + HEADERSIZE];

            //add header to first 12 bytes
            for(int i=0; i<HEADERSIZE; i++){
                fullFrame[i]=header[i];
            }

            //add basePacket to rest
            for (int i = HEADERSIZE; i < fullFrame.Length; i++)
            {
                fullFrame[i] = basePacket[i-HEADERSIZE];
            }

            return fullFrame;

        }

        
        //Form frame
        //create header for each frame
        //encapsulate 
    }*/
}
