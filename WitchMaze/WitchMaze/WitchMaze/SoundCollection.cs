using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.Sound
{
    public class SoundCollection
    {
        public SoundEffectInstance menuSound { get; set; }
        SoundEffect menuSoundeffect;

        public SoundEffectInstance inGameSound { get; set; }
        SoundEffect inGameSoundeffect;

        public SoundEffect klick { get; protected set; }
        public SoundEffect klick2 { get; protected set; }
        public SoundEffect teleport { get; protected set; }
        public SoundEffect itemCollected { get; protected set; }
        public SoundEffect bounce { get; protected set; }
        public SoundEffect gameStateChange { get; protected set; }

        public SoundCollection()
        {
            klick = Game1.getContent().Load<SoundEffect>("Sound/Sounds/menuClick");
            klick2 = Game1.getContent().Load<SoundEffect>("Sound/Sounds/MenuClick2");
            teleport = Game1.getContent().Load<SoundEffect>("Sound/Sounds/teleport2");
            itemCollected = Game1.getContent().Load<SoundEffect>("Sound/Sounds/itemCollected");
            bounce = Game1.getContent().Load<SoundEffect>("Sound/Sounds/bounce");
            gameStateChange = Game1.getContent().Load<SoundEffect>("Sound/Sounds/GameStateChange");

            menuSoundeffect = Game1.getContent().Load<SoundEffect>("Sound/BackgroundMusic/ThemeFromWitchmazeMenu");
            menuSound = menuSoundeffect.CreateInstance();
            menuSound.IsLooped = true;

            inGameSoundeffect = Game1.getContent().Load<SoundEffect>("Sound/BackgroundMusic/ThemeFromWitchmazeInGame");
            inGameSound = inGameSoundeffect.CreateInstance();
            inGameSound.IsLooped = true;
        }

        public void setVolume(float volume){
            menuSound.Volume = volume;
            inGameSound.Volume = volume;
        }
    }
}
