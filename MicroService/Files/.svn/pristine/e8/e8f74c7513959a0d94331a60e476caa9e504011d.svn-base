using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Files.Service
{
    public class ETag
    {
        private int CHUNK_SIZE = 1 << 22;//=4*1024*1024=4M 检查文件大小,大于切割后计算,小于直接计算

        public byte[] sha1(byte[] data)
        {
            return System.Security.Cryptography.SHA1.Create().ComputeHash(data);
        }

        public String urlSafeBase64Encode(byte[] data)
        {
            String encodedString = Convert.ToBase64String(data);
            encodedString = encodedString.Replace('+', '-').Replace('/', '_');
            return encodedString;
        }

        public String calcETag(byte[] fileData)
        {
            String etag = "";
            if (fileData.Length <= CHUNK_SIZE)
            {
                byte[] sha1Data = sha1(fileData);
                int sha1DataLen = sha1Data.Length;
                byte[] hashData = new byte[sha1DataLen + 1];
                System.Array.Copy(sha1Data, 0, hashData, 1, sha1DataLen);
                hashData[0] = 0x16;
                etag = urlSafeBase64Encode(hashData);
            }
            else {
                int chunkCount = (int)(fileData.Length / CHUNK_SIZE);
                if (fileData.Length % CHUNK_SIZE != 0)
                {
                    chunkCount += 1;
                }
                byte[] allSha1Data = new byte[0];
                for (int i = 0; i < chunkCount; i++)
                {
                    byte[] bytesRead = new byte[CHUNK_SIZE];
                    System.Array.Copy(fileData, i* CHUNK_SIZE, bytesRead, 0, CHUNK_SIZE);
                    byte[] chunkDataSha1 = sha1(bytesRead);
                    byte[] newAllSha1Data = new byte[chunkDataSha1.Length
                            + allSha1Data.Length];
                    System.Array.Copy(allSha1Data, 0, newAllSha1Data, 0,
                            allSha1Data.Length);
                    System.Array.Copy(chunkDataSha1, 0, newAllSha1Data,
                            allSha1Data.Length, chunkDataSha1.Length);
                    allSha1Data = newAllSha1Data;
                }
                byte[] allSha1DataSha1 = sha1(allSha1Data);
                byte[] hashData = new byte[allSha1DataSha1.Length + 1];
                System.Array.Copy(allSha1DataSha1, 0, hashData, 1,
                        allSha1DataSha1.Length);
                hashData[0] = (byte)0x96;
                etag = urlSafeBase64Encode(hashData);
            }
            return etag;
        }


        public String calcETag(String path)
        {
            String etag = "";
            FileStream fs;
            fs = File.OpenRead(path);
            long fileLength = fs.Length;
            if (fileLength <= CHUNK_SIZE)
            {
                byte[] fileData = new byte[(int)fileLength];
                fs.Read(fileData, 0, (int)fileLength);
                byte[] sha1Data = sha1(fileData);
                int sha1DataLen = sha1Data.Length;
                byte[] hashData = new byte[sha1DataLen + 1];

                System.Array.Copy(sha1Data, 0, hashData, 1, sha1DataLen);
                hashData[0] = 0x16;
                etag = urlSafeBase64Encode(hashData);
            }
            else
            {
                int chunkCount = (int)(fileLength / CHUNK_SIZE);
                if (fileLength % CHUNK_SIZE != 0)
                {
                    chunkCount += 1;
                }
                byte[] allSha1Data = new byte[0];
                for (int i = 0; i < chunkCount; i++)
                {
                    byte[] chunkData = new byte[CHUNK_SIZE];
                    int bytesReadLen = fs.Read(chunkData, 0, CHUNK_SIZE);
                    byte[] bytesRead = new byte[bytesReadLen];
                    System.Array.Copy(chunkData, 0, bytesRead, 0, bytesReadLen);
                    byte[] chunkDataSha1 = sha1(bytesRead);
                    byte[] newAllSha1Data = new byte[chunkDataSha1.Length
                            + allSha1Data.Length];
                    System.Array.Copy(allSha1Data, 0, newAllSha1Data, 0,
                            allSha1Data.Length);
                    System.Array.Copy(chunkDataSha1, 0, newAllSha1Data,
                            allSha1Data.Length, chunkDataSha1.Length);
                    allSha1Data = newAllSha1Data;
                }
                byte[] allSha1DataSha1 = sha1(allSha1Data);
                byte[] hashData = new byte[allSha1DataSha1.Length + 1];
                System.Array.Copy(allSha1DataSha1, 0, hashData, 1,
                        allSha1DataSha1.Length);
                hashData[0] = (byte)0x96;
                etag = urlSafeBase64Encode(hashData);
            }
            fs.Close();
            return etag;

        }

    }
}
