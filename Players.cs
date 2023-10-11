using System;
using System.IO;
using System.Collections.Generic;

namespace FinalProject
{
    public class Players
    {
        public string name;
        public int wins, losses, ties;
        public Players(string name, int wins, int losses, int ties)
        {
            this.name = name;
            this.wins = wins;
            this.losses = losses;
            this.ties = ties;
        }
        //create class methods to get attributes
        public string getName()
        {
            return this.name;
        }
        public string setName(string newName)
        {
            this.name = newName;
            return this.name;
        }
        public int getWins()
        {
            return this.wins;
        }
        public int setWins(int newWins)
        {
            this.wins = newWins;
            return this.wins;
        }
        public int getLosses()
        {
            return this.losses;
        }
        public int setLosses(int newLosses)
        {
            this.losses = newLosses;
            return this.losses;
        }
        public int getTies()
        {
            return this.ties;
        }
        public int setTies(int newTies)
        {
            this.ties = newTies;
            return this.ties;
        }
        
    }
}
