using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blacksmith
{
    public class LocalizationTestComponent : MonoBehaviour
    {
        public LocalizedTMPText m_Text;

        public void Start()
        {
            m_Text.Display();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}