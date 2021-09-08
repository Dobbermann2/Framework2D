using Engine2D.Physics;
using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine2D
{
    public class Scene
    {

        Dictionary<int, Entity> entities;

        public Dictionary<Type, List<Component>> Components { get; set; }
        Dictionary<Type, System> systems;

        private int uniqueID = 0;
        private int UniqueID
        {
            get { return ++uniqueID; }
        }

        public Scene()
        {
            Physics2D.Init();
            entities = new Dictionary<int, Entity>();
            Components = new Dictionary<Type, List<Component>>();
            systems = new Dictionary<Type, System>();

            systems.Add(typeof(TagComponent), new TagSystem());
            systems.Add(typeof(TransformComponent), new TransformSystem());
            systems.Add(typeof(SpriteComponent), new SpriteSystem());
            systems.Add(typeof(RigidBodyComponent), new RigidBodySystem());
            systems.Add(typeof(BoxCollider2DComponent), new BoxCollider2DSystem());
            systems.Add(typeof(ScriptComponent), new ScriptSystem());

            foreach (Type t in systems.Keys)
            {
                Components.Add(t, new List<Component>());
            }
        }

        bool haltUpdate = false;

        public void Update(float deltaTime) { 
            foreach(KeyValuePair<Type, System> s in systems)
            {
                foreach (Component comp in Components[s.Key])
                {
                    s.Value.PreUpdate(comp);
                    s.Value.Update(comp, deltaTime);
                    if (haltUpdate)
                    {
                        haltUpdate = false;
                        return;
                    }
                }
            }
        }

        public void Draw(BatchRenderer batchRenderer)
        {
            foreach (KeyValuePair<Type, System> s in systems)
            {
                foreach (Component comp in Components[s.Key])
                    s.Value.Draw(comp, batchRenderer);
            }
        }

        public Entity CreateEntity()
        {
            Entity e = new Entity(this, UniqueID);
            entities.Add(e.EntityID, e);
            e.AddComponent<TransformComponent>();
            return e;
        }

        private Entity CreateEmptyEntity(int entityID)
        {
            Entity e = new Entity(this, entityID);
            entities.Add(e.EntityID, e);
            return e;
        }

        internal Entity GetOrCreateEmptyEntity(int entityID)
        {
            Entity e;
            if(entities.TryGetValue(entityID, out e))
            {
                return e;
            }
            e = CreateEmptyEntity(entityID);
            return e;
            
        }

        public T GetComponent<T>(Entity entity) where T : Component
        {
            List<Component> entityComponents = Components[typeof(T)];
            Component component = entityComponents.Where(x => x.Entity.EntityID == entity.EntityID).First();
            return (T) component;
        }

        public T AddComponent<T>(Entity entity) where T : Component, new()
        {
            if (HasComponent<T>(entity)) return null;
            Component e;
            Components[typeof(T)].Add((e = new T()));
            e.Entity = entity;
            return (T) e;
        }

        public bool HasComponent<T>(Entity entity) where T : Component
        {
            return (Components[typeof(T)].Where(x => x.Entity.EntityID == entity.EntityID).Count() > 0);
        }

        public void Clear()
        {
            uniqueID = 0;
            foreach(List<Component> lC in Components.Values)
            {
                lC.Clear();
            }
            entities.Clear();
            haltUpdate = true;
        }

        struct EntityComponent
        {
            public Entity entity;
            public Component component;
        }
    }
}
