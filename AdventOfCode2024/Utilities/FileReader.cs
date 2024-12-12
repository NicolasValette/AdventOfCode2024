using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024.Utilities
{
    public class FileReader
    {
        private const string INPUTPATH = "E:\\Dev\\ProjetsVisual\\AdventOfCode2024\\AdventOfCode2024\\Ressources\\Input\\";
        private string _fileName;
        StreamReader _stream;
        bool _verbose = false;

        public bool EndOfStream => _stream.EndOfStream;

        public FileReader(string fileName, bool verbose = false)
        {
            _fileName = fileName;
            _verbose = verbose;
            string path = Path.Combine(INPUTPATH, _fileName);
            if (_verbose) Console.WriteLine($"Reading file : {path}");
            _stream = new StreamReader(path);
        }
        public void Close()
        {
            _stream.Close();
        }
        public string Read()
        {
            return _stream.ReadLine();
        }

        public List<string> ReadAndSplitInto2DList()
        {
            List<string> finalList = new List<string>();
            do
            {
                string line = _stream.ReadLine();
                finalList.Add(line.Trim());
            } while (!_stream.EndOfStream);

            return finalList;
        }
        public string[] ReadToEndAndSplit()
        {
            
            string[] lines = _stream.ReadToEnd().Split('\n').Select(x=>x.Trim()).ToArray();
            return lines;
        }
        public char[][] ReadToEndAndSplitInto2DCharArray()
        {
            
            var lines = _stream.ReadToEnd().Split('\n').Select(x=>x.Trim()).ToArray();
            var charArray = lines.Select(x => x.ToArray()).ToArray();
            return charArray;
        }
        public List<string> ReadToEndAndSplit(char separator)
        {
            var lines = _stream.ReadToEnd().Split(separator).Select(x=>x.Trim()).ToList();
            return lines;
        }
        public string ReadToEnd()
        {
            string line = _stream.ReadToEnd().Trim();
            return line;
        }

        public char ReadChar()
        {
            return (char)_stream.Read();
        }
    }
}
