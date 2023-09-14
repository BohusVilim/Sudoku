using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class NumberService
    {
        private List<Number> _numbers = new List<Number>();
        public List<Number> Numbers
        {
            get { return _numbers; }
            set { _numbers = value; }
        }

        public List<Number> NumbersGenerator()
        {
            for (int i = 1; i < 82; i++)
            {
                do
                {
                    if (_numbers.Any(a => a.Value == 0))
                    {
                        var numberOfRow = _numbers.Where(b => b.Value == 0).FirstOrDefault().Row;
                        var rowForRemove = _numbers.Where(a => a.Row == numberOfRow).ToList();

                        foreach (var nmbr in rowForRemove)
                        {
                            _numbers.Remove(nmbr);
                        }

                        for (int a = 1; a < 10 ; a++)
                        {
                            var newNumber = new Number();
                            newNumber.Id = _numbers.Count + 1;
                            newNumber.Row = RowAssigner(newNumber.Id);
                            newNumber.Column = ColumnAssigner(newNumber.Id);
                            newNumber.Section = SectionAssigner(newNumber);
                            newNumber.Value = ValueAssigner(newNumber);

                            _numbers.Add(newNumber);
                        }

                        goto here;
                    }

                    var number = new Number();
                    number.Id = i;
                    number.Row = RowAssigner(i);
                    number.Column = ColumnAssigner(i);
                    number.Section = SectionAssigner(number);
                    number.Value = ValueAssigner(number);

                    _numbers.Add(number);

                here: continue;
                }
                while (_numbers.Any(a => a.Value == 0));
            }
            
            return _numbers;
        }

        public Enums.Rows RowAssigner(int id)
        {
            if (id < 10)
                return Enums.Rows.R1;

            if (id < 19)
                return Enums.Rows.R2;

            if (id < 28)
                return Enums.Rows.R3;

            if (id < 37)
                return Enums.Rows.R4;

            if (id < 46)
                return Enums.Rows.R5;

            if (id < 55)
                return Enums.Rows.R6;

            if (id < 64)
                return Enums.Rows.R7;

            if (id < 73)
                return Enums.Rows.R8;

            return Enums.Rows.R9;
        }

        public Enums.Columns ColumnAssigner(int id)
        {
            int i = (id - 1) % 9 + 1;

            switch (i)
            {
                case 1:
                    return Enums.Columns.A;
                case 2:
                    return Enums.Columns.B;
                case 3:
                    return Enums.Columns.C;
                case 4:
                    return Enums.Columns.D;
                case 5:
                    return Enums.Columns.E;
                case 6:
                    return Enums.Columns.F;
                case 7:
                    return Enums.Columns.G;
                case 8:
                    return Enums.Columns.H;
                case 9:
                    return Enums.Columns.I;
                default:
                    throw new ArgumentOutOfRangeException("Fucked up!");
            }
        }

        public Enums.Sections SectionAssigner(Number number)
        {
            if ((number.Row == Enums.Rows.R1 || number.Row == Enums.Rows.R2 || number.Row == Enums.Rows.R3)
                && (number.Column == Enums.Columns.A || number.Column == Enums.Columns.B || number.Column == Enums.Columns.C))
                return Enums.Sections.S1;

            if ((number.Row == Enums.Rows.R1 || number.Row == Enums.Rows.R2 || number.Row == Enums.Rows.R3)
                && (number.Column == Enums.Columns.D || number.Column == Enums.Columns.E || number.Column == Enums.Columns.F))
                return Enums.Sections.S2;

            if ((number.Row == Enums.Rows.R1 || number.Row == Enums.Rows.R2 || number.Row == Enums.Rows.R3)
                && (number.Column == Enums.Columns.G || number.Column == Enums.Columns.H || number.Column == Enums.Columns.I))
                return Enums.Sections.S3;

            if ((number.Row == Enums.Rows.R4 || number.Row == Enums.Rows.R5 || number.Row == Enums.Rows.R6)
                && (number.Column == Enums.Columns.A || number.Column == Enums.Columns.B || number.Column == Enums.Columns.C))
                return Enums.Sections.S4;

            if ((number.Row == Enums.Rows.R4 || number.Row == Enums.Rows.R5 || number.Row == Enums.Rows.R6)
                && (number.Column == Enums.Columns.D || number.Column == Enums.Columns.E || number.Column == Enums.Columns.F))
                return Enums.Sections.S5;

            if ((number.Row == Enums.Rows.R4 || number.Row == Enums.Rows.R5 || number.Row == Enums.Rows.R6)
                && (number.Column == Enums.Columns.G || number.Column == Enums.Columns.H || number.Column == Enums.Columns.I))
                return Enums.Sections.S6;

            if ((number.Row == Enums.Rows.R7 || number.Row == Enums.Rows.R8 || number.Row == Enums.Rows.R9)
                && (number.Column == Enums.Columns.A || number.Column == Enums.Columns.B || number.Column == Enums.Columns.C))
                return Enums.Sections.S7;

            if ((number.Row == Enums.Rows.R7 || number.Row == Enums.Rows.R8 || number.Row == Enums.Rows.R9)
                && (number.Column == Enums.Columns.D || number.Column == Enums.Columns.E || number.Column == Enums.Columns.F))
                return Enums.Sections.S8;

                return Enums.Sections.S9;
        }

        public int ValueAssigner(Number number)
        {
            var random = new Random();
            int value;
            bool valueValidation = false;

            int i = 0;

            do
            {
                i++;

                value = random.Next(1, 10);
                valueValidation = NumberValueValidator(number, value);

                if (i > 25)
                {
                    return 0;
                }
            }
            while (valueValidation == false);

            return value;
        }

        public bool NumberValueValidator(Number number, int value)
        {
            if (RowValidator(number, value) == false || ColumnValidator(number, value) == false || SectionValidator(number, value) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RowValidator(Number number, int value)
        {
            if (_numbers.Where(a => a.Row == number.Row).Select(b => b.Value).Contains(value))
                return false;

            else
                return true;
        }

        public bool ColumnValidator(Number number, int value)
        {
            if (_numbers.Where(a => a.Column == number.Column).Select(b => b.Value).Contains(value))
                return false;

            else
                return true;
        }

        public bool SectionValidator(Number number, int value)
        {
            if (_numbers.Where(a => a.Section == number.Section).Select(b => b.Value).Contains(value))
                return false;

            else
                return true;
        }
    }
}
