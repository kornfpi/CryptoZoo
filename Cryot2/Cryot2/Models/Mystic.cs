using System;
using System.Collections.Generic;
using System.Text;

namespace Cryot2.Models
{
    class Mystic
    {
        public string name;
        public int speed;
        public int health;
        public int power;

        public int buffSpeed;
        public int buffHealth;
        public int buffPower;

        public int wands;
        public int potions;
        public int crystals;

        public bool isBuffed;

        public Mystic(string input_name, int input_speed, int input_health, int input_power)
        {
            this.name = input_name;

            this.speed = input_speed;
            this.health = input_health;
            this.power = input_power;

            this.buffSpeed = 0;
            this.buffHealth = 0;
            this.buffPower = 0;

            this.isBuffed = false;

            this.wands = 0;
            this.potions = 0;
            this.crystals = 0;
        }

        public void setBuffStats()
        {
            if (this.isBuffed)
            {
                this.buffSpeed = this.wands > 0 ? this.health + this.wands : this.health;
                this.buffHealth = this.potions > 0 ? this.health + this.potions : this.health;
                this.buffPower = this.crystals > 0 ? this.health + this.crystals : this.health;
                this.buffSpeed = this.speed + this.wands;
                this.buffPower = this.power + this.crystals;
            }
        }

        public void addBuffs(int input_wands, int input_potions, int input_crystals)
        {
            this.isBuffed = true;
            this.wands = input_wands;
            this.potions = input_potions;
            this.crystals = input_crystals;
        }


    }
}
