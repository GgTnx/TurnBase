namespace PlayerScripts
{
    public class Autogun : Weapon
    {
        public int dmg;
        public float _speedBullet;

        public Autogun(int DMG, float speedBullet)
        {
            dmg = DMG;
            _speedBullet = speedBullet;
        }

    }
}