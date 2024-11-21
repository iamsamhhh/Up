using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFramework
{    
    public abstract partial class MonoBehaviourSimplify : MonoBehaviour
    {
        public GUIManager guiManager{
            get { return GUIManager.instance; }
        }

        #region Runner

        Callback _update;

        internal void OnUpdate(Callback update) {
            _update += update;
            Runner.instance.SFUpdate(update);
        }

        internal void OnStart(Callback start){
            Runner.instance.SFStart(start);
        }

        internal void OnAwake(Callback awake){
            Runner.instance.SFAwake(awake);
        }

        internal void RemoveAllLoacalUpdates(){
            Runner.instance.RemoveUpdate(_update);
            _update = null;
        }

        #endregion
        public void Show()
        {
            GameObjectSimplify.Show(gameObject);
        }

        public void Hide()
        {
            GameObjectSimplify.Hide(gameObject);
        }

        public void Identity()
        {
            TransformSimplify.Identity(transform);
        }

        public void Delay(float seconds, Callback onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds, Callback onFinished)
        {
            yield return new WaitForSeconds(seconds);

            onFinished();
        }

        #region msgCenter

        private Dictionary<string, Callback<object>> meventsDict = new Dictionary<string, Callback<object>>();

        #region Add event

        private void AddEventCheck(string eventType){
            if (!meventsDict.ContainsKey(eventType))
            {
                meventsDict.Add(eventType, null);
            }
        }

        // public void AddEvent(string eventType, Callback callback)
        // {
        //     MsgCenter.AddEvent(eventType, callback);
        // }

        public void AddEvent(string eventType, Callback<object> callback)
        {
            AddEventCheck(eventType);
            MsgCenter.AddEvent(eventType, callback);
            meventsDict[eventType] += callback;
        }

        // public void AddEvent<A, B>(string eventType, Callback<A, B> callback)
        // {
        //     MsgCenter.AddEvent(eventType, callback);
        // }

        // public void AddEvent<A, B, C>(string eventType, Callback<A, B, C> callback)
        // {
        //     MsgCenter.AddEvent(eventType, callback);
        // }

        // public void AddEvent<A, B, C, D>(string eventType, Callback<A, B, C, D> callback)
        // {
        //     MsgCenter.AddEvent(eventType, callback);
        // }

        // public void AddEvent<A, B, C, D, E>(string eventType, Callback<A, B, C, D, E> callback)
        // {
        //     MsgCenter.AddEvent(eventType, callback);
        // }

        #endregion

        #region remove event

        private void RemoveEventCheck(string eventType){
            if (meventsDict.ContainsKey(eventType))
            {
                Callback<object> d = meventsDict[eventType];

                if (d == null)
                {
                    throw new Exception(string.Format("该事件{0}没有委托，无法移除", eventType));
                }
            }
            else
            {
                throw new Exception(string.Format("事件{0}不存在", eventType));
            }
        }

        // public void RemoveEvent(string eventType, Callback callback)
        // {
        //     MsgCenter.RemoveEvent(eventType, callback);
        // }


        public void RemoveEvent(string eventType, Callback<object> callback)
        {
            RemoveEventCheck(eventType);
            MsgCenter.RemoveEvent(eventType, callback);
            meventsDict[eventType] -= callback;
        }


        // public void RemoveEvent<A, B>(string eventType, Callback<A, B> callback)
        // {
        //     MsgCenter.RemoveEvent(eventType, callback);
        // }


        // public void RemoveEvent<A, B, C>(string eventType, Callback<A, B, C> callback)
        // {
        //     MsgCenter.RemoveEvent(eventType, callback);
        // }


        // public void RemoveEvent<A, B, C, D>(string eventType, Callback<A, B, C, D> callback)
        // {
        //     MsgCenter.RemoveEvent(eventType, callback);
        // }


        // public void RemoveEvent<A, B, C, D, E>(string eventType, Callback<A, B, C, D, E> callback)
        // {
        //     MsgCenter.RemoveEvent(eventType, callback);
        // }

        #endregion

        #region Broadcast event

        // public void BroadcastEvent(string eventType)
        // {
        //     MsgCenter.BroadcastEvent(eventType);
        // }

        public void BroadcastEvent(string eventType, object arg)
        {
            MsgCenter.BroadcastEvent(eventType, arg);
        }

        // public void BroadcastEvent<A, B>(string eventType, A arg1, B arg2)
        // {
        //     MsgCenter.BroadcastEvent(eventType, arg1, arg2);
        // }

        // public void BroadcastEvent<A, B, C>(string eventType, A arg1, B arg2, C arg3)
        // {
        //     MsgCenter.BroadcastEvent(eventType, arg1, arg2, arg3);
        // }

        // public void BroadcastEvent<A, B, C, D>(string eventType, A arg1, B arg2, C arg3, D arg4)
        // {
        //     MsgCenter.BroadcastEvent(eventType, arg1, arg2, arg3, arg4);
        // }

        // public void BroadcastEvent<A, B, C, D, E>(string eventType, A arg1, B arg2, C arg3, D arg4, E arg5)
        // {
        //     MsgCenter.BroadcastEvent(eventType, arg1, arg2, arg3, arg4, arg5);
        // }

        #endregion

        public void RemoveAllLocalEvents(){
            foreach (var record in meventsDict){
                RemoveEventCheck(record.Key);
                MsgCenter.RemoveEvent(record.Key, record.Value);
            }
            meventsDict.Clear();
        }

        #endregion
        
    }
}