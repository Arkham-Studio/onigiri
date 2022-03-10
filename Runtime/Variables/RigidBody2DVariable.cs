using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/RigidBody2D")]
    public class RigidBody2DVariable : BaseVariable<Rigidbody2D>
    {
        public void SetVelocity(Vector2Variable _v) => Value.velocity = _v;
        public void SetVelocityX(ChangeableVariable _x) => Value.velocity = new Vector2(((IVariableValueTo)_x).ValueToFloat(), Value.velocity.y);
        public void SetVelocityY(ChangeableVariable _y) => Value.velocity = new Vector2(Value.velocity.x, ((IVariableValueTo)_y).ValueToFloat());

        public void AddForce(Vector2Variable _v) => Value.AddForce(_v, ForceMode2D.Force);
        public void AddForceX(ChangeableVariable _x) => Value.AddForce(new Vector2(((IVariableValueTo)_x).ValueToFloat(), 0), ForceMode2D.Force);
        public void AddForceY(ChangeableVariable _y) => Value.AddForce(new Vector2(0, ((IVariableValueTo)_y).ValueToFloat()), ForceMode2D.Force);

        public void AddImpulse(Vector2Variable _v) => Value.AddForce(_v, ForceMode2D.Impulse);
        public void AddImpulseX(ChangeableVariable _x) => Value.AddForce(new Vector2(((IVariableValueTo)_x).ValueToFloat(), 0), ForceMode2D.Impulse);
        public void AddImpulseY(ChangeableVariable _y) => Value.AddForce(new Vector2(0, ((IVariableValueTo)_y).ValueToFloat()), ForceMode2D.Impulse);
    }
}
