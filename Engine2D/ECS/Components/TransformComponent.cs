using Newtonsoft.Json.Linq;
using OpenTK.Mathematics;

namespace Engine2D
{
    public class TransformComponent : Component
    {

        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get; set; } = 0f;


        public override void Serialize(JObject obj)
        {
            JObject array = new JObject();
            base.Serialize(obj);
            array["eID"] = Entity.EntityID;
            array["pX"] = Position.X;
            array["pY"] = Position.Y;
            array["sX"] = Scale.X;
            array["sY"] = Scale.Y;
            array["R"] = Rotation;
            obj["TransformComponent"] = array;
        }

        public override void Deserialize(JObject obj)
        {
            Position = new Vector2((float)obj["pX"], (float)obj["pY"]);
            Scale = new Vector2((float)obj["sX"], (float)obj["sY"]);
            Rotation = (float) obj["R"];
        }
    }
}
