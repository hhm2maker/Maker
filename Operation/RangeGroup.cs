using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public class RangeGroup : List<int>
    {
        public RangeGroup(String str,char splitNotation, char rangeNotation)
        {
            string[] strSplit = str.Split(splitNotation);
            for (int i = 0; i < strSplit.Length; i++)
            {
                if (strSplit[i].Contains(rangeNotation))
                {
                    String[] TwoNumber = null;
                    TwoNumber = strSplit[i].Split(rangeNotation);

                    int One = int.Parse(TwoNumber[0]);
                    int Two = int.Parse(TwoNumber[1]);
                    if (One < Two)
                    {
                        for (int k = One; k <= Two; k++)
                        {
                            this.Add(k);
                        }
                    }
                    else if (One > Two)
                    {
                        for (int k = One; k >= Two; k--)
                        {
                            this.Add(k);
                        }
                    }
                    else
                    {
                        this.Add(One);
                    }
                }
                else
                {
                    this.Add(int.Parse(strSplit[i]));
                }
            }
        }
    }
}
