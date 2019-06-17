using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace Catalog
{
    class Song : Item
    {
        public string Singer { get; set; }
        public int LengthInSeconds { get; set; }

        public  void Play()
        {
            if (Location == null)
            {
                return;
            }
            SoundPlayer sound = new SoundPlayer(Location);
            sound.Play();
        }
    }
}
