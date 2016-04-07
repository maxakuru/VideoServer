using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SE3314Assignment1
{
    class MJPEGVideo
    {
        private string filename;
        //int frameStart;
        //int frameEnd;
        FileStream fileStream;
        //int numBytesRead;
        //int bytesToRead;

        public MJPEGVideo(string name)
        {
            this.filename = name;
            //frameStart = 0;
            //frameEnd = 5;
            //numBytesRead = 0;
            //bytesToRead = 5;

            //Get the file
            string target = @"c:\videos";
            Environment.CurrentDirectory = (target);
            string path = Directory.GetCurrentDirectory();
            Console.Write(path);
            fileStream = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);

        }

        public string getName()
        {
            return filename;
        }

        public void disposeVideo()
        {
            this.fileStream.Dispose();
            this.fileStream.Close();
        }

        public void remakeVideo()
        {
            this.fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /*
        public byte[] GetNextFrame()
        {
            byte[] ba = new byte[400000];
            int size = 0;
            int ex = fileStream.Read(ba, frameStart, frameEnd);
            if (ex != 0)
            {
                byte[] r = new byte[] { ba[frameStart], ba[frameStart + 1], ba[frameStart + 2], ba[frameStart + 3], ba[frameStart + 4] };
                for (int i = 0; i < 5; i++)
                {
                    size += (int)r[i];
                }
                frameStart = frameStart + 5;
                frameEnd = frameEnd + size;
                
                int ex2 = fileStream.Read(ba, frameStart, frameEnd);
                if (ex2!=0)
                {
                    byte[] temp = new byte[ex2];
                    Array.Copy(ba, 0, temp, 0, ex2);
                    return temp;
                }
                frameStart += frameEnd;
            }

            return null;
        }*/

        
        public byte[] GetNextFrame()
        {
            int length = 0;
            byte[] frame_length = new byte[5];

            //read current frame length
            fileStream.Read(frame_length,0,5);
	
            //transform frame_length to integer
            try
            {
                length = int.Parse(System.Text.Encoding.UTF8.GetString(frame_length));
            }
            catch(FormatException exc)
            {
                //string doesn't exist because the file's over
                length = 0;
            }

            byte[] arr = null;
            if (length > 0)
            {
                arr = new byte[length];
                fileStream.Read(arr, 0, length);
            }
            else
            {
                //disposeVideo();
                fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                arr = GetNextFrame();
            }

            return arr;
        }
    
         
        /*
        public byte[] GetNextFrame()
        {
            //First read the 5 bytes of length
            byte[] bytesIn = new byte[5];
            bytesToRead = 5;
            numBytesRead = 0;
            //
            int n=0;
            while ((n = fileStream.Read(bytesIn, numBytesRead, bytesToRead - numBytesRead)) > 0)
               numBytesRead += n;  // sum is a buffer offset for next reading
             //int n = fileStream.Read(bytesIn, 0, bytesToRead);

             if (n == 0)
             {
                //this happens if the end of the file is reached
                fileStream.Close();
                fileStream.Dispose();
                fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                numBytesRead = 0;
                //GetNextFrame();
             }

            bytesToRead = 0;

            char char1 = (char)bytesIn[0];
            char char2 = (char)bytesIn[1];
            char char3 = (char)bytesIn[2];
            char char4 = (char)bytesIn[3];
            char char5 = (char)bytesIn[4];
            string nn = char1.ToString()+char2.ToString()+char3.ToString()+char4.ToString()+char5.ToString();
            int bytesToRead2 = Int32.Parse(nn);

            /*
            for (int i = 0; i < 5; i++)
            {
                bytesToRead += (int)bytesIn[i];
            }

            numBytesRead = 0;
            bytesIn = new byte[bytesToRead2];

            n = 0;
            while ((n = fileStream.Read(bytesIn, numBytesRead, bytesToRead2 - numBytesRead)) > 0)
                numBytesRead += n;  // sum is a buffer offset for next reading
            //int n = fileStream.Read(bytesIn, 0, bytesToRead);

            if (n == 0)
            {
                //this happens if the end of the file is reached
                fileStream.Close();
                fileStream.Dispose();
                fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                numBytesRead = 0;
                //GetNextFrame();
            }

            bytesToRead2 = 0;
            return bytesIn;
        }*/
    }
}
