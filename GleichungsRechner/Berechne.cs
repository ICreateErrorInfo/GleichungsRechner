using System;
using System.Collections.Generic;
using System.Text;

namespace GleichungsRechner
{
    public class Berechne
    {
        public static List<string> _z = new List<string>();
        public static List<string> _o = new List<string>();
        public static char[] Zahlen = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public static char[] Operatoren = { '-', '+', '*', '/', '^', '(', ')' };
        public static double _ergebnis = 0;

        public static void Parser(string input)
        {
            char[] inputCh;
            inputCh = input.ToCharArray();

            for (int i = 0; i < inputCh.Length; i++)
            {
                if (isOperator(inputCh[i]))
                {
                    _o.Add(inputCh[i].ToString());
                }
                else if (isZahl(inputCh[i]))
                {
                    if(i + 1 < inputCh.Length)
                    {
                        if (isZahl(inputCh[i]) && isZahl(inputCh[i + 1]))
                        {
                            _z.Add((Convert.ToDouble(inputCh[i].ToString()) * 10 + Convert.ToDouble(inputCh[i + 1].ToString())).ToString());
                            i += 1;
                        }
                        else
                        {
                            _z.Add(inputCh[i].ToString());
                        }
                    }
                    else
                    {
                        _z.Add(inputCh[i].ToString());
                    }
                }
            }
        }
        public static void VorzeichenBerechner()
        {
            if (_o.Count == _z.Count)
            {
                double erstezahl = Convert.ToInt32(_z[0]);
                if (_o[0] == "-")
                {
                    erstezahl = erstezahl - (erstezahl * 2);
                    _z[0] = erstezahl.ToString();
                    _o.RemoveAt(0);
                }
                else
                {
                    _o.RemoveAt(0);
                }
            }
        }
        public static void PlusZuMinusConverter()
        {
            for (int x = 0; _o.Count - 1 > x; x++)
            {
                double zDou = Convert.ToDouble(_z[x + 1]);
                if (_o[x] == "-")
                {
                    zDou = zDou - (zDou * 2);
                    _z[x + 1] = zDou.ToString();
                }
            }
        }
        public static void PunktVorStrich()
        {
            bool firstTime = true;

            for(; _o.Count > 0;)
            {
                if (_o.Contains("^"))
                {
                    int stelle = _o.IndexOf("^");
                    if (firstTime)
                    {
                        _ergebnis = Convert.ToDouble(_z[stelle + 1]);
                        firstTime = false;
                        _z.RemoveAt(stelle + 1);
                    }
                    _ergebnis = Berechner("^", _ergebnis.ToString(), _z[stelle]);
                    _o.RemoveAt(stelle);
                    _z.RemoveAt(stelle);
                    continue;
                }

                if (_o.Contains("*"))
                {
                    int stelle = _o.IndexOf("*");
                    if (firstTime)
                    {
                        _ergebnis = Convert.ToDouble(_z[stelle + 1]);
                        firstTime = false;
                        _z.RemoveAt(stelle + 1);
                    }
                    _ergebnis = Berechner("*", _z[stelle], _ergebnis.ToString());
                    _o.RemoveAt(stelle);
                    _z.RemoveAt(stelle);
                    continue;
                }

                if (_o.Contains("/"))
                {
                    int stelle = _o.IndexOf("/");
                    if (firstTime)
                    {
                        _ergebnis = Convert.ToDouble(_z[stelle + 1]);
                        firstTime = false;
                        _z.RemoveAt(stelle + 1);
                    }
                    _ergebnis = Berechner("/", _z[stelle], _ergebnis.ToString());
                    _o.RemoveAt(stelle);
                    _z.RemoveAt(stelle);
                    continue;
                }

                if (_o.Contains("+"))
                {
                    int stelle = _o.IndexOf("+");
                    if (firstTime)
                    {
                        _ergebnis = Convert.ToDouble(_z[stelle + 1]);
                        firstTime = false;
                        _z.RemoveAt(stelle + 1);
                    }
                    _ergebnis = Berechner("+", _z[stelle], _ergebnis.ToString());
                    _o.RemoveAt(stelle);
                    _z.RemoveAt(stelle);
                    continue;
                }

                if (_o.Contains("-"))
                {
                    int stelle = _o.IndexOf("-");
                    if (firstTime)
                    {
                        _ergebnis = Convert.ToDouble(_z[stelle + 1]);
                        firstTime = false;
                        _z.RemoveAt(stelle + 1);
                    }
                    _ergebnis = Berechner("-", _z[stelle], _ergebnis.ToString());
                    _o.RemoveAt(stelle);
                    _z.RemoveAt(stelle);
                    continue;
                }
            }
        }                   

        public static double Term(string input)
        {
            Parser(input);
            VorzeichenBerechner();
            PlusZuMinusConverter();
            PunktVorStrich();
 
            return _ergebnis;
        }

        public static bool isOperator(char input)
        {
            int i = 0;
            foreach(int element in Operatoren)
            {
                if(input == Operatoren[i])
                {
                    return true;
                }
                i++;
            }
            return false;
        } // check ob input is operator
        public static bool isZahl(char input)
        {
            int i = 0;
            foreach (int element in Zahlen)
            {
                if (input == Zahlen[i])
                {
                    return true;
                }
                i++;
            }
            return false;
        } // che<ck ob input is zahl
        public static double Berechner(string Operator, string zahl, string zahl2)
        {
            var ergebnis = Convert.ToDouble(zahl);
            var zahlDou = Convert.ToDouble(zahl2);

            if (Operator == "^")
            {
                for (int x = 1; zahlDou > x; x++)
                {
                    ergebnis = ergebnis * ergebnis;
                }
            }
            else if (Operator == "*")
            {
                ergebnis = ergebnis * zahlDou;
            }
            else if (Operator == "/")
            {
                ergebnis = ergebnis / zahlDou;
            }
            else if (Operator == "+")
            {
                ergebnis = ergebnis + zahlDou;
            }
            else if (Operator == "-")
            {
                ergebnis = ergebnis + zahlDou;
            }
            return ergebnis;
        } //vorzeichen zuordnen       
    }
}
