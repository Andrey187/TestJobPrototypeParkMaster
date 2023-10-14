using System.Collections.Generic;
using UnityEngine;

public class MovementSequenceManager : MonoBehaviour, IUndo
{
    private Dictionary<IPlayer, IDrawController> _playerSwitch = new Dictionary<IPlayer, IDrawController>();
    private bool anyCharacterMoving;

    private void Start()
    {
        EventManagers.Instance.AddToDictionary += FillingOutTheDictionary;
        SceneReloadEvent.Instance.UnsubscribeEvents.AddListener(UnsubscribeEvents);
    }

    protected void UnsubscribeEvents()
    {
        EventManagers.Instance.AddToDictionary -= FillingOutTheDictionary;
    }

    private void Update()
    {
        //foreach (IPlayer obj in _playerSwitch.Keys)
        //{
        //    if (obj.IsMoving)
        //    {
        //        anyCharacterMoving = true;
        //        break;// If at least one character moves, exit the loop
        //    }
        //    else
        //    {
        //        anyCharacterMoving = false;
        //    }
        //}

        // Set CharacterMoving for all DrawControllers
        foreach (IDrawController draw in _playerSwitch.Values)
        {
            draw.CharacterMoving = anyCharacterMoving;
        }
    }

    private void FillingOutTheDictionary(IPlayer character, IDrawController draw)
    {
        if (!_playerSwitch.ContainsKey(character))
        {
            _playerSwitch.Add(character, draw);
            draw.PathDrawingCompleted += MultiFollowing;
        }
    }

    private void MultiFollowing()
    {
        foreach (KeyValuePair<IPlayer, IDrawController> entry in _playerSwitch)
        {
            IPlayer obj = entry.Key;
            IDrawController draw = entry.Value;
            if (/*obj.IsMoving &&*/ draw.CountOfTries <= 0)
            {
                obj.ResetPosition();
                obj.FollowPath();
            }
        }
    }

    public void Undo()
    {
        _playerSwitch.Clear();
    }
}