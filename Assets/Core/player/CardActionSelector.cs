using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace core
{
    public class CardActionSelector : MonoBehaviour
    {
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private TMP_Text cardTitle;
        [SerializeField] private Button buttonClone;

        private List<Button> _buttons = new();
        public bool IsOpen { get; private set; }
        public void Open(Vector3 pos, Card card)
        {
            actionPanel.transform.position = pos;
            actionPanel.SetActive(true);
            cardTitle.text = card.CardName;
            _buttons = new List<Button>();
            foreach (var action in card.Actions)
            {
                var button = Instantiate(buttonClone, actionPanel.transform);
                button.GetComponentInChildren<TMP_Text>().text = action.Text;
                button.onClick.AddListener(delegate { CreateAction(action);});
                _buttons.Add(button);
            }

            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
            actionPanel.SetActive(false);
            for (var i = 0; i < _buttons.Count; i++)
            {
                Destroy(_buttons[i].gameObject);
                _buttons.Remove(_buttons[i]);
            }
        }

        private void CreateAction(CardAction action)
        {
            // TODO: ask for player instruction on non-simple actions
            Board.Instance.PlayEffect(action.GenerateEffect());
            Close();
        }
    }
}