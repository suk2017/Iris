using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;

public class GZipArc
{

    /// <summary>
    /// 压缩为ZIP 可以是路径 也可以是文件
    /// </summary>
    /// <param name="SourceName">P 或者 P F E</param>
    /// <param name="TargetName">P F E</param>
    public static void _Zip(string SourceName, string TargetName)
    {
        if (Directory.Exists(SourceName))//如果是目录
        {
            //using (FileStream fileStream = File.OpenRead(SourceName))
            //{
                using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(TargetName)))
                {
                    _ZipAll(SourceName, zipOutputStream);
                }
            //}
        }
        else//如果是文件
        {
            _ZipFile(SourceName);
        }
    }

    /// <summary>
    /// 压缩单个文件
    /// </summary>
    /// <param name="fileName">文件 P.F.E.</param>
    private static void _ZipFile(string fileName)
    {
        using (FileStream fileStream = File.OpenRead(fileName))
        {
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(fileStream))
            {
                byte[] buffer = new byte[4096];//4K设为一个缓冲区
                ZipEntry entry = new ZipEntry(fileName.Replace(Path.GetPathRoot(fileName), ""));// 此处去掉盘符，如D:\123\1.txt 去掉D:
                entry.DateTime = DateTime.Now;
                zipOutputStream.PutNextEntry(entry);
                int sourceBytes;
                do
                {
                    sourceBytes = fileStream.Read(buffer, 0, buffer.Length);//返回值是读取到的数据长度
                    zipOutputStream.Write(buffer, 0, sourceBytes);
                } while (sourceBytes > 0);
            }

        }
    }

    /// <summary>
    /// 压缩文件夹 递归调用
    /// </summary>
    /// <param name="source">源目录P </param>
    /// <param name="_zipOutputStream">ZipOutputStream对象</param>
    private static void _ZipAll(string dirName, ZipOutputStream _zipOutputStream)
    {
        string[] filenames = Directory.GetFileSystemEntries(dirName);//获取当前文件夹内的（文件名和文件夹名）

        foreach (string file in filenames)
        {
            if (Directory.Exists(file))//如果是文件夹
            {
                _ZipAll(file, _zipOutputStream);// 递归压缩子文件夹
            }
            else//如果是文件
            {
                using (FileStream fileStream = File.OpenRead(file))
                {
                    byte[] buffer = new byte[4096];//4K设为一个缓冲区
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));// 此处去掉盘符，如D:\123\1.txt 去掉D:
                    entry.DateTime = DateTime.Now;
                    _zipOutputStream.PutNextEntry(entry);
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fileStream.Read(buffer, 0, buffer.Length);//返回值是读取到的数据长度
                        _zipOutputStream.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }
        }

    }

    // TODO 解压到内存
    /// <summary>
    /// 从ZIP解压缩
    /// </summary>
    /// <param name="sourceFile">文件全名 P F E</param>
    /// <param name="targetPath">解压路径 P</param>
    public static bool _UnZip(string sourceFile, string targetPath)
    {
        if (!File.Exists(sourceFile))//文件不存在
        {
            throw new FileNotFoundException(string.Format("未能找到文件 '{0}' ", sourceFile));
        }
        if (!Directory.Exists(targetPath))//目标路径不存在
        {
            Directory.CreateDirectory(targetPath);//创建路径
        }
        using (var _ZipInputStream = new ZipInputStream(File.OpenRead(sourceFile)))
        {
            try
            {
                ZipEntry theEntry;
                while ((theEntry = _ZipInputStream.GetNextEntry()) != null)
                {
                    if (theEntry.IsDirectory)//是目录
                    {
                        continue;//就跳过
                    }
                    string directorName = Path.Combine(targetPath, Path.GetDirectoryName(theEntry.Name));
                    string fileName = Path.Combine(directorName, Path.GetFileName(theEntry.Name));
                    if (!Directory.Exists(directorName))//若没有这个目录 则创建（好像是给后来的文件解压使用的 因为不可能一开始就有目录）
                    {
                        Directory.CreateDirectory(directorName);
                    }
                    if (!String.IsNullOrEmpty(fileName))//如果有效
                    {
                        using (FileStream streamWriter = File.Create(fileName))
                        {
                            int size = 4096;
                            byte[] data = new byte[size];
                            while (size > 0)
                            {
                                size = _ZipInputStream.Read(data, 0, data.Length);
                                streamWriter.Write(data, 0, size);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Inspire_UI.debuglog(e.ToString());
                Debug.Log(e.ToString());
                throw;
            }
            
        }
        return true;
    }

    /// <summary>
    /// 存储ZIP格式压缩文件 但是后缀名自定义
    /// </summary>
    /// <param name="filename">完整的格式名</param>
    /// <param name="value">数据</param>
    //public static void SaveCompressedFile(string filename, string value)
    //{
    //    //创建一个 FileStream 对象 
    //    FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);

    //    //创建一个 GZipStream 对象，Compress 表示压缩基础流。 
    //    GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress);

    //    //实现一个 write，使其以一种特定的编码向流中写入字符。
    //    StreamWriter writer = new StreamWriter(compressionStream);


    //    //写数据
    //    writer.Write(value);

    //    //释放资源
    //    writer.Close();
    //    compressionStream.Close();
    //    fileStream.Close();
    //    writer.Dispose();
    //    compressionStream.Dispose();
    //    fileStream.Dispose();

    //}

    /// <summary>
    /// 载入ZIP格式的压缩文件
    /// </summary>
    /// <param name="filename">完整的文件名</param>
    //public static string LoadCompressedFile(string filename)
    //{
    //    //创建一个 FileStream 对象
    //    FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

    //    //创建一个 GZipStream 对象，Decompress 表示解压缩基础流。
    //    GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);

    //    //实现从字符串进行读取 
    //    StreamReader reader = new StreamReader(compressionStream);

    //    //获取数据
    //    string data = reader.ReadToEnd();

    //    //释放资源
    //    fileStream.Close();
    //    compressionStream.Close();
    //    reader.Close();
    //    fileStream.Dispose();
    //    compressionStream.Dispose();
    //    reader.Dispose();

    //    //返回数据
    //    return data;
    //}

    //public static void Main(string[] args)
    //{
    //    try
    //    {
    //        //定义文件路径
    //        string filename = @"c:/compressedFile.txt";
    //        Console.WriteLine("Enter a string to compress (will be repeated 10 times):");
    //        //在控制台上输入文字
    //        string sourceString = Console.ReadLine();
    //        StringBuilder sourceStringMultiplier = new StringBuilder(sourceString.Length * 100);
    //        for (int i = 0; i < 100; i++)
    //        {
    //            sourceStringMultiplier.Append(sourceString);
    //        }
    //        sourceString = sourceStringMultiplier.ToString();
    //        Console.WriteLine("Source data is {0} bytes long.", sourceString.Length);
    //        SaveCompressedFile(filename, sourceString);
    //        Console.WriteLine("/nData saved to {0}.", filename);
    //        FileInfo compressedFileData = new FileInfo(filename);
    //        Console.WriteLine("Compressed file is {0} bytes long.", compressedFileData.Length);
    //        string recoveredString = LoadCompressedFile(filename);
    //        recoveredString = recoveredString.Substring(0, recoveredString.Length / 100);
    //        Console.WriteLine("/nRecovered data: {0}", recoveredString);
    //        Console.ReadKey();
    //    }
    //    catch (IOException ex)
    //    {
    //        Console.WriteLine("An IO exception has been thrown!");
    //        Console.WriteLine(ex.ToString()); Console.ReadKey();
    //    }
    //}
}




