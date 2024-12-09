using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2024.Days
{
    public class DiskBlock : IEquatable<DiskBlock>
    {
        public int Id;
        public int Size;
        public bool IsFreeSpace;
        public DiskBlock(int id, int size, bool isFreeSpace)
        {
            Id = isFreeSpace?-1:id;
            Size = size;
            IsFreeSpace = isFreeSpace;
        }

        public bool Equals(DiskBlock other)
        {
            return Id == other.Id && Size == other.Size && IsFreeSpace == other.IsFreeSpace;
        }

        public override bool Equals(object obj)
        {
            return obj is DiskBlock other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Size;
                hashCode = (hashCode * 397) ^ IsFreeSpace.GetHashCode();
                return hashCode;
            }
        }
    }
    public class DiskUsage
    {
        private int _firstFreeSpace = -1;
        private int _lastFileBlock = -1;
        
        private List<string> _disk;
        private List<DiskBlock> _diskBlocks;

        public int FirstFreeSpace => _firstFreeSpace;
        public int LastFileBlock => _lastFileBlock;
        public List<string> Disk => _disk;
        public List<DiskBlock> DiskBlock => _diskBlocks;
        public DiskUsage()
        {
            _disk = new List<string>();
            _diskBlocks = new List<DiskBlock>();
        }

        private void AddBlockOrFreeSpace(string element)
        {
            _disk.Add(element);
        }
        public void AddBlock(int id)
        {
            _lastFileBlock = _disk.Count;
            AddBlockOrFreeSpace(id.ToString());
        }

        public void AddBlock(int id, int size, bool isFreeSpace)
        {
            if (isFreeSpace && size <= 0) return;
            DiskBlock block = new DiskBlock(id, size, isFreeSpace);
            _diskBlocks.Add(block);
        }
        public void AddFreeSpace()
        {
            if (_firstFreeSpace == -1)
                _firstFreeSpace = _disk.Count;
            AddBlockOrFreeSpace(".");
        }

        public void Invert()
        {
            _disk[_firstFreeSpace] = _disk[_lastFileBlock];
            _disk[_lastFileBlock] = ".";

            _firstFreeSpace =_disk.FindIndex(x=>x == ".");
            _lastFileBlock = _disk.FindLastIndex(x=>x != ".");
        }

        public bool MoveFileBloc(int idFile, int idFreeSpace)
        {
            int blockLeft = _diskBlocks[idFreeSpace].Size - _diskBlocks[idFile].Size;
            _diskBlocks[idFreeSpace].Size = _diskBlocks[idFile].Size;
            _diskBlocks[idFreeSpace].Id = _diskBlocks[idFile].Id;
            _diskBlocks[idFreeSpace].IsFreeSpace = false;
          
            _diskBlocks[idFile].Id = -1;
            _diskBlocks[idFile].IsFreeSpace = true;

            if (blockLeft > 0)
            {
                var block = new DiskBlock(-1, blockLeft, true);
                _diskBlocks.Insert(idFreeSpace+1, block);
                return true;
            }

            return false;
        }
        
        public long Compress()
        {
            // Console.WriteLine(ToString2());
            // Console.WriteLine("SIZE : " + Size());
            for (int i = _diskBlocks.Count - 1; i > 0; i--)
            {
                // Console.WriteLine("******************************************************************************************************"); 
                // Console.WriteLine(ToString2());
                // Console.WriteLine("******************************************************************************************************");
                var block = _diskBlocks[i];
                if (block.IsFreeSpace) continue;
                var index = _diskBlocks.FindIndex(x => x.IsFreeSpace && x.Size >= block.Size);
                if (index != -1 && index < i)
                {
                    var freeSpace = _diskBlocks[index];

                    if (MoveFileBloc(i, index))
                    {
                        i++;
                    }
                    
                }
            }
            // Console.WriteLine("******************************************************************************************************"); 
            // Console.WriteLine(ToString2());
            // Console.WriteLine("SIZE : " + Size());
            return CheckSum2();
        }

        public long Size()
        {
            long size = 0;
            foreach (var element in _diskBlocks)
            {
                size += element.Size;
            }

            return size;
        }
        public long CheckSum2()
        {
            long solution = 0;
            int pos = 0;
            foreach (var element in _diskBlocks)
            {
                for (int i = 0; i < element.Size; i++)
                {
                    if (!element.IsFreeSpace) solution += pos * element.Id;
                    
                    pos++;
                }
            }

            return solution;
        }
        public long CheckSum()
        {
            long solution = 0;
            for (int i=0; i<_disk.Count && _disk[i] != "." ; i++)
            {
                solution += i * long.Parse(_disk[i]);
            }

            return solution;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var s in _disk)
            {
                stringBuilder.Append(s);
            }
            return  stringBuilder.ToString();
        }
        public string ToString1Bis()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string prev = "";
            int size = 0;
            foreach (var s in _disk)
            {
                if (prev == string.Empty)
                {
                    prev = s;
                }

                if (s == prev)
                    size++;
                else
                {
                    stringBuilder.Append(size);
                    prev = s;
                    size = 1;
                }
            }
            return  stringBuilder.ToString();
        }
        public string ToString2()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var element in _diskBlocks)
            {
                stringBuilder.Append("{");
                for (int i = 0; i < element.Size; i++)
                {
                    stringBuilder.Append(element.IsFreeSpace?"-":(element.Id.ToString()));
                }
                stringBuilder.Append("}");
            }
            return  stringBuilder.ToString();
        }
    }
}