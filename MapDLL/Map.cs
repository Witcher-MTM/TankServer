using System;
using System.IO;

namespace MapDLL
{
    public class Map
    {
        public char[,] IntMap { set; get; }
        public Map()
        {
            IntMap = new char[17, 32]{
                    {'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ','X',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ','X',' ','X'},
                    {'X',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X',' ',' ','X'},
                    {'X',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X',' ',' ','X'},
                    {'X',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ','X'},
                    {'X',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X',' ',' ','X'},
                    {'X',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X',' ',' ','X'},
                    {'X',' ','X',' ','X','X','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','X','X',' ','X',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'}
                };
           
            
        }
        public Map(char[,] IntMap)
        {
            this.IntMap = IntMap;
        }
        public void SaveMap()
        {
           
            if (!Directory.Exists(@"C:\ProgramData\RubickTanks"))
            {
                Directory.CreateDirectory(@"C:\ProgramData\RubickTanks");
            }
            if (!File.Exists(@"C:\ProgramData\RubickTanks\TanksMap.txt"))
            {
                string map = string.Empty;
                for (int i = 0; i < IntMap.GetLength(0); i++)
                {
                    for (int j = 0; j < IntMap.GetLength(1); j++)
                    {
                        map += IntMap[i, j];
                    }
                    map += "\n";
                }
                File.AppendAllText(@"C:\ProgramData\RubickTanks\TanksMap.txt", map);
            }
        }
        public void LoadMap()
        {
            string map;
            int row = 0;
            int column = 0;
            if (Directory.Exists(@"C:\ProgramData\RubickTanks"))
            {
                if (File.Exists(@"C:\ProgramData\RubickTanks\TanksMap.txt"))
                {
                    map = File.ReadAllText(@"C:\ProgramData\RubickTanks\TanksMap.txt");
                    for (int i = 0; i < map.Length; i++)
                    {
                        if(map[i] == '\n')
                        {
                            row++;
                            column = 0;
                        }
                        else
                        {
                            IntMap[row, column] = map[i];
                            column++;
                        }
                    }
                }
            }
        }

    }
}
