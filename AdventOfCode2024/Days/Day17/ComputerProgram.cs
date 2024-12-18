using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2024.Days.Day17
{
    public enum Instruction
    {
        Adv,
        Bxl,
        Bst,
        Jnz,
        Bxc,
        Out,
        Bdv,
        Cdv
    }
    public class ComputerProgram
    {
        private long _registerA;
        private long _registerB;
        private long _registerC;
        private readonly List<int> _program;
        private readonly List<long> _output = new List<long>();
        private int _instructionPointer = 0;

        private bool _verbose = false;

        public ComputerProgram(long a, long b, long c, string programString)
        {
            var list = programString.Split(',');

            _program = programString.Split(',').Select(x => int.Parse(x.Trim())).ToList();

            _registerA = a;
            _registerB = b;
            _registerC = c;
        }

        private long ComboOperand(long operand)
        {
            if (operand <= 3)
                return operand;
            if (operand == 4)
                return _registerA;
            if (operand == 5)
                return _registerB;
            if (operand == 6)
                return _registerC;
            return operand;
        }

        private string PrintOutput()
        {
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendLine("The result of the program is");
            if (_output.Count == 0)
            {
                stringBuilder.AppendLine("Empty");
                return stringBuilder.ToString();
            }

            stringBuilder.Append(_output[0]);
            for (int i=1; i<_output.Count;i++)
            {
                stringBuilder.Append(',');
                stringBuilder.Append(_output[i]);
            }

            //stringBuilder.AppendLine();
           // stringBuilder.AppendLine("END");
            return stringBuilder.ToString();
        }

        public string Run(bool verbose = false)
        {
            _verbose = verbose;
            while (_instructionPointer >= 0 && _instructionPointer < _program.Count)
            {
                switch ((Instruction)_program[_instructionPointer])
                {
                    case Instruction.Adv:
                        ADivision(_program[_instructionPointer+1]);
                        break;
                    case Instruction.Bdv:
                        BDivision(_program[_instructionPointer+1]);
                        break;
                    case Instruction.Bst:
                        Bst(_program[_instructionPointer+1]);
                        break;
                    case Instruction.Bxc:
                        BitwiseXorC(_program[_instructionPointer+1]);
                        break;
                    case Instruction.Bxl:
                        BitwiseXorL(_program[_instructionPointer+1]);
                        break;
                    case Instruction.Cdv:
                        CDivision(_program[_instructionPointer+1]);
                        break;
                    case Instruction.Jnz:
                        Jump(_program[_instructionPointer+1]);
                        break;
                    case Instruction.Out:
                        Out(_program[_instructionPointer+1]);
                        break;
                    default :
                        return "ERROR";
                }
                
            }

            return PrintOutput();
        }

        private void PrintRegister()
        {
            Console.WriteLine($"-------------------------");
            Console.WriteLine($"REGISTER A : {_registerA}");
            Console.WriteLine($"REGISTER B : {_registerB}");
            Console.WriteLine($"REGISTER C : {_registerC}");
            Console.WriteLine($"-------------------------");
        }
        #region Instruction

        /// <summary>
        /// ADV
        /// </summary>
        /// <param name="operand">Combo Operand</param>
        private void ADivision(long operand)
        {
            long numerator = (long)Math.Pow(2, ComboOperand(operand));
            if (_verbose) Console.WriteLine($"Instruction ADV : Register A =  {_registerA} / {numerator}");
            _registerA = _registerA / numerator;
            _instructionPointer += 2;
            if (_verbose) PrintRegister();
        }
        
        /// <summary>
        /// BDV
        /// </summary>
        /// <param name="operand">Combo Operand</param>
        private void BDivision (long operand)
        {
            long numerator = (long)Math.Pow(2, ComboOperand(operand));
            if (_verbose) Console.WriteLine($"Instruction BDV : Register B = {_registerA} / {numerator}");
            _registerB = _registerA / numerator;
            _instructionPointer += 2;
            if (_verbose) PrintRegister();
        }
        
        /// <summary>
        /// CDV
        /// </summary>
        /// <param name="operand">Combo Operand</param>
        private void CDivision (long operand)
        {
            long numerator = (long)Math.Pow(2, ComboOperand(operand));
            if (_verbose) Console.WriteLine($"Instruction CDV : Register C = {_registerA} / {numerator}");
            _registerC = _registerA / numerator;
            _instructionPointer += 2;
            if (_verbose) PrintRegister();
        }

        /// <summary>
        /// BXL
        /// </summary>
        /// <param name="operand">Litteral operand</param>
        private void BitwiseXorL(long operand)
        {
            if (_verbose) Console.WriteLine($"Instruction BXL : Register B = {_registerB} ^ {operand}");
            _registerB = _registerB ^ operand;
            _instructionPointer += 2;
            if (_verbose) PrintRegister();
        }

        /// <summary>
        /// BST
        /// </summary>
        /// <param name="operand">Combo operand</param>
        private void Bst(long operand)
        {
            if (_verbose) Console.WriteLine($"Instruction BST : Register B = {ComboOperand(operand)} % 8 ");
            _registerB = ComboOperand(operand) % 8;
            _instructionPointer += 2;
            if (_verbose) PrintRegister();
        }

        /// <summary>
        /// Jump
        /// </summary>
        /// <param name="operand">Litteral operand</param>
        private void Jump(long operand)
        {
            if (_verbose) Console.WriteLine($"Instruction Jump To ");
            if (_registerA == 0)
            {
                if (_verbose) Console.WriteLine($"=> Nothing");
                _instructionPointer += 2;
                return;
            }

            _instructionPointer = (int)operand;
            if (_verbose) Console.WriteLine($"=> {_instructionPointer}");
            if (_verbose) PrintRegister();
        }

        /// <summary>
        /// BXC
        /// </summary>
        /// <param name="operand">ignored</param>
        private void BitwiseXorC(long operand)
        {
            if (_verbose) Console.WriteLine($"Instruction BXC : Register B = {_registerB} ^ {_registerC}");
            _registerB = _registerB ^ _registerC;
            _instructionPointer += 2;
            if (_verbose) PrintRegister();
        }

        /// <summary>
        /// Out
        /// </summary>
        /// <param name="operand">Combo operand</param>
        private void Out(long operand)
        {
            if (_verbose) Console.WriteLine($"Instruction OUT : printing => {ComboOperand(operand)}");
            _output.Add(ComboOperand(operand) % 8);
            _instructionPointer += 2;
            if (_verbose) PrintRegister();
        }
        #endregion
        
    }
    
    
}