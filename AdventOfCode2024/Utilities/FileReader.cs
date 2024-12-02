﻿using System;
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
        private string _fileName;
        StreamReader _stream;
        bool _verbose = false;

        public FileReader(string fileName, bool verbose = false)
        {
            _fileName = fileName;
            _verbose = verbose;
            string path = Path.Combine("E:\\Dev\\ProjetsVisual\\AdventOfCode2024\\AdventOfCode2024\\Ressources\\Input\\", _fileName);
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
        public string[] ReadToEndAndSplit()
        {
            string[] lines = _stream.ReadToEnd().Split('\n');
            return lines;
        }
    }
}