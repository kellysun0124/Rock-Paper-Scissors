using System;
using System.Collections.Generic;
using System.IO;

namespace FinalProject
{
    public class DataLoader
    {
        public static List<Players> playerList()
        {
            List<Players> playerList = new List<Players>();
            using(var readfile = new StreamReader("player_log.csv"))
            {
                while(!readfile.EndOfStream){
                    string PlayerDataLine = readfile.ReadLine();
                    var PlayerData = PlayerDataLine.Split(",");
                    if(string.IsNullOrEmpty(PlayerDataLine)){
                        continue;
                    }
                    try{
                        string name = PlayerData[0];
                        int wins = int.Parse(PlayerData[1]);
                        int losses = int.Parse(PlayerData[2]);
                        int ties = int.Parse(PlayerData[3]);
                        
                        Players player = new Players(name, wins, losses, ties);
                        playerList.Add(player);
                    } catch (Exception e){
                        Console.WriteLine("There was a problem reading the Player's file");
                        Console.WriteLine(e);
                    }
                }
            }
            return playerList;
        }
    }
}