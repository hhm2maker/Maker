using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public static class IntCollection
    {
        public static List<List<int>> VerticalIntList = new List<List<int>>()
        {
            new List<int>() { -1, 80, 70, 60, 50, 40, 30, 20, 10, -1 },
            new List<int>() { 91, 81, 71, 61, 51, 41, 31, 21, 11, 1 },
            new List<int>() {  92, 82, 72, 62, 52, 42, 32, 22, 12, 2 },
            new List<int>() {  93, 83, 73, 63, 53, 43, 33, 23, 13, 3 },
            new List<int>() {  94, 84, 74, 64, 54, 44, 34, 24, 14,4 },
            new List<int>() {  95, 85, 75, 65, 55, 45, 35, 25, 15, 5 },
            new List<int>() {  96, 86, 76, 66, 56, 46, 36, 26, 16, 6 },
            new List<int>() {  97, 87, 77, 67, 57, 47, 37, 27, 17,7 },
            new List<int>() {  98, 88, 78, 68, 58, 48, 38, 28, 18, 8 },
            new List<int>() { -1,89, 79, 69, 59, 49, 39, 29, 19 ,-1}
        };
        public static List<List<int>> HorizontalIntList = new List<List<int>>()
        {
           new List<int>() { -1, 28, 29, 30, 31, 32, 33, 34, 35, -1 },
           new List<int>() { 108, 64, 65, 66, 67, 96, 97, 98, 99, 100 },
           new List<int>() { 109, 60, 61, 62, 63, 92, 93, 94, 95, 101 },
           new List<int>() { 110, 56, 57, 58, 59, 88, 89, 90, 91, 102 },
           new List<int>() { 111, 52, 53, 54, 55, 84, 85, 86, 87, 103 },
           new List<int>() { 112, 48, 49, 50, 51, 80, 81, 82, 83, 104 },
           new List<int>() { 113, 44, 45, 46, 47, 76, 77, 78, 79, 105 },
           new List<int>() { 114, 40, 41, 42, 43, 72, 73, 74, 75, 106 },
           new List<int>() { 115, 36, 37, 38, 39, 68, 69, 70, 71, 107 } ,
           new List<int>() { -1,116, 117, 118, 119, 120, 121, 122, 123, -1 },
        };
    }
}
