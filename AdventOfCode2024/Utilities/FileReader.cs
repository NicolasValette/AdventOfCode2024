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
            List<string> _finalList = new List<string>();
            do
            {
                string line = _stream.ReadLine();
                _finalList.Add(line.Trim());
            } while (!_stream.EndOfStream);

            return _finalList;
        }
        public string[] ReadToEndAndSplit()
        {
            string[] lines = _stream.ReadToEnd().Split('\n');
            return lines;
        }
        public string ReadToEnd()
        {
            string line = _stream.ReadToEnd().Trim();
            return line;
        }
    }
}
