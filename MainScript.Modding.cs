using System;
using System.Reflection;
using System.Collections;
using static MagmaMc.Modding.Health;
using UnityEngine;
using System.Diagnostics;

namespace MagmaMc.Modding
{
    public interface IMod
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }

        /// <summary>
        /// Set At Runtime
        /// </summary>
        public string CurrentScene { get; set; }
    }
    [Serializable]
    public class ModInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }


#if !UnityMode
    [Obsolete("Unity Only", true)]
#endif
    [Serializable]
    public enum ModEvents
    {
       SceneChanged = 0,
       PlayerJoined = 1,
       PlayerLeft = 2,
       PlayerDamaged = 3,
       PlayerDestroyed = 4,
    }


    /// <summary>
    /// Executes Method On Load
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class InitializeMethodAttribute : Attribute
    { }

    /// <summary>
    /// Executes Method On Update
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UpdateMethodAttribute : Attribute
    { }

    /// <summary>
    /// <seealso cref="string"/> SceneName
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
#if !UnityMode
    [Obsolete("Unity Only", true)]
#endif
    public class EventMethodAttribute : Attribute
    { }

#if PA && UnityMode
    public class EnemyHandler
    {
        public EnemyHandler() { }

    }
    public class EnemyData
    {
        public string Name { get; set; }
        public EnemySpawnType SpawnType { get; set; }
        public EnemyType Type { get; set; }
    }

    public class Health
    {
        public GameObject gameObject { get; set; }
        public uint MaxHealth { get; set; }
        public uint HP { get; set; }
        public byte Regen { get; set; }
        public uint Damage(ToolType Tool, EnemyData Enemy, ushort TargetDamage)
        {
            uint Damage = TargetDamage;
            if (Tool == ToolType.Sword) Damage = (uint)(TargetDamage * 1.3f);
            else if (Tool == ToolType.PickAxe) Damage = (uint)(TargetDamage * 0.9f);
            else if (Tool == ToolType.Shovel) Damage = (uint)(TargetDamage * 0.7f);
            if (HP - Damage <= 0) 
                Remove(); 
            else
                HP -= Damage;


            return Damage;
        }

        public void Add(uint Health)
        {
            uint Temp = HP + Health;
            if (HP > MaxHealth) Temp = MaxHealth;
            HP = Temp;
        }

        public Health(GameObject gameObject, uint MaxHealth, uint HP, byte Regen)
        {
            this.gameObject = gameObject;
            this.MaxHealth = MaxHealth;
            this.HP = HP;
        }

        public IEnumerator StartSystem()
        {
            Stopwatch Delay = new Stopwatch();
            yield return new WaitForFixedUpdate(); // Let Load
            Delay.Start();
            while (true)
            {
                if (Delay.ElapsedMilliseconds/1000.0 > 2.0) { Delay.Restart(); Add(Regen); } // Regen
                if (HP <= 0)
                {
                    Remove();
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        public void Remove() => gameObject.AddComponent<MarkAsDelete>();
    }
    
    public enum EnemySpawnType
    {
        Common,
        Rare,
        Epic
    }

    public enum EnemyType
    {
        Weak,
        Normal,
        Strong
    }
    public enum ToolType
    {
        Axe,
        PickAxe,
        Sword,
        Shovel
    }


#endif
}