using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFramework{
    public class Runner : MonoSingletonBaseAuto<Runner>{
        
        private Callback _update, _start, _awake;
        private bool started = false, awaken = false;

        public void SFUpdate(Callback update) {
            _update += update;
        }

        public void RemoveUpdate(Callback update){
            _update -= update;
        }

        public void SFStart(Callback start){
            _start += start;
        }

        public void SFAwake(Callback awake){
            _awake += awake;
        }

        private void Update() {

            if (!awaken && _awake != null){
                _awake();
                awaken = true;
            }

            if (!started && _start != null){
                _start();
                started = true;
            }

            if (_update == null) return;

            _update();
        }

        private void OnDestroy() {
            _update = null;
            _awake = null;
            _start = null;
        }
    }
}