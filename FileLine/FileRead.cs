using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLine
{
    class FileRead
    {
        private FileStream fs1;
        private string temp;

        public FileRead()
        {
            temp = "C:\\Users\\LZ\\Desktop\\1.txt";
            fs1 = new FileStream(temp, FileMode.Create, FileAccess.Write, FileShare.None);            
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="farr">bytes</param>
        /// <param name="count">length</param>
        public void write(byte[] farr, int count)
        {
            fs1.Write(farr, 0, count);
        }

        /// <summary>
        /// 删除临时文件
        /// </summary>
        /// <param name="des">临时文件的转移位置</param>
        public void fileMove(string des)
        {
            File.Copy(temp,des);
            File.Delete(temp);
        }

        /// <summary>
        /// 释放资源，解除占用
        /// </summary>
        public void dispose()
        {
            try
            {
                fs1.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}

