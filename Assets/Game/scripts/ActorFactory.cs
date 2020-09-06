using UnityEngine;

namespace TinyBitTurtle
{
    public enum CharacterType
    {
        fighter,// player
        wizard,
        thief,
        cleric,
        skeleton,// enemies
        cyclop,
        rat,

        max_character,
    }

    public interface ICharacterFactory
    {
        Actor Create(CharacterType characterType);
    }

    public class ActorFactory : SingletonMonoBehaviour<ActorFactory>, ICharacterFactory
    {
        public Actor Create(CharacterType characterType)
        {
            Actor newCharacter = null;

            switch (characterType)
            {
                case CharacterType.fighter:
                    newCharacter = gameObject.AddComponent<Friendly>();
                    newCharacter.SetCharacterSettings("FighterSettings");
                    newCharacter.SetControl(InputCtrl.Instance.playerControl);
                    break;

                case CharacterType.thief:
                    newCharacter = gameObject.AddComponent<Friendly>();
                    newCharacter.SetCharacterSettings("ThiefSettings");
                    newCharacter.SetControl(InputCtrl.Instance.playerControl);
                    break;

                case CharacterType.wizard:
                    newCharacter = gameObject.AddComponent<Friendly>();
                    newCharacter.SetCharacterSettings("WizardSettings");
                    newCharacter.SetControl(InputCtrl.Instance.playerControl);
                    break;

                case CharacterType.cleric:
                    newCharacter = gameObject.AddComponent<Friendly>();
                    newCharacter.SetCharacterSettings("ClericSettings");
                    newCharacter.SetControl(InputCtrl.Instance.playerControl);
                    break;

                case CharacterType.skeleton:
                    newCharacter = gameObject.AddComponent<Enemy>();
                    newCharacter.SetCharacterSettings("SkeletonSettings");
                    newCharacter.SetControl(InputCtrl.Instance.AIControl);
                    break;

                case CharacterType.rat:
                    newCharacter = gameObject.AddComponent<Enemy>();
                    newCharacter.SetCharacterSettings("RatSettings");
                    newCharacter.SetControl(InputCtrl.Instance.AIControl);
                    break;

                default:
                    newCharacter = (Actor)null;
                    break;
            }

            newCharacter.aggroCircle = gameObject.transform.Find("AggroCircle").gameObject;

            return newCharacter;
        }
    }
}