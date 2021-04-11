namespace Blacksmith
{
    public class SoundHelper
    {
        #region Methods
        //PUBLIC
        public static SoundSpawnerComponent GetSoundSpawner()
        {
            if (SoundManagerComponent.TryGetInstance(out SoundManagerComponent instance))
            {
                if (instance.TryGetFakeSingleton(out SoundSpawnerComponent soundSpawner))
                {
                    return soundSpawner;
                }
            }
            return null;
        }

        public static bool TryGetSoundSpawner(out SoundSpawnerComponent soundSpawner)
        {
            soundSpawner = GetSoundSpawner();
            return (soundSpawner != null);
        }
        //PROTECTED
        //PRIVATE
        #endregion
    }
}