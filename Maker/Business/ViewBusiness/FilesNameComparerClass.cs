using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business
{
    ///<summary>
    ///主要用于文件名的比较。
    ///</summary>
    public class FilesNameComparerClass : IComparer
    {

        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        ///<summary>
        ///比较两个字符串，如果含用数字，则数字按数字的大小来比较。
        ///</summary>
        ///<param name="x"></param>
        ///<param name="y"></param>
        ///<returns></returns>
        int IComparer.Compare(Object x, Object y)
        {
            if (x == null || y == null)
                throw new ArgumentException("Parameters can't be null");

            string fileA = x as string;
            string fileB = y as string;
            char[] arr1 = fileA.ToCharArray();
            char[] arr2 = fileB.ToCharArray();

            int i = 0, j = 0;
            while (i < arr1.Length && j < arr2.Length)
            {
                if (char.IsDigit(arr1[i]) && char.IsDigit(arr2[j]))
                {
                    string s1 = "", s2 = "";
                    while (i < arr1.Length && char.IsDigit(arr1[i]))
                    {
                        s1 += arr1[i];
                        i++;
                    }
                    while (j < arr2.Length && char.IsDigit(arr2[j]))
                    {
                        s2 += arr2[j];
                        j++;
                    }

                    if (int.Parse(s1) > int.Parse(s2))
                    {
                        return 1;
                    }

                    if (int.Parse(s1) < int.Parse(s2))
                    {
                        return -1;
                    }

                }
                else
                {
                    if (arr1[i] > arr2[j])
                    {
                        return 1;
                    }

                    if (arr1[i] < arr2[j])
                    {
                        return -1;
                    }
                    i++;
                    j++;

                }

            }

            if (arr1.Length == arr2.Length)
            {
                return 0;
            }
            else
            {
                return arr1.Length > arr2.Length ? 1 : -1;
            }

            //            return string.Compare( fileA, fileB );
            //            return( (new CaseInsensitiveComparer()).Compare( y, x ) );
        }
    }

}
