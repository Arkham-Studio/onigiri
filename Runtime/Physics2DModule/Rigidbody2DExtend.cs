using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using Arkham.Onigiri.Utils;

namespace Arkham.Onigiri.Physics2DModule
{
    public class Rigidbody2DExtend : BaseComponentExtend<Rigidbody2D>
    {


        public void SetVelocity(Vector2Variable _v) => myExtendedComponent.velocity = _v;
        public void SetVelocityX(ChangeableVariable _x) => myExtendedComponent.velocity = new Vector2(((IVariableValueTo)_x).ValueToFloat(), myExtendedComponent.velocity.y);
        public void SetVelocityY(ChangeableVariable _y) => myExtendedComponent.velocity = new Vector2(myExtendedComponent.velocity.x, ((IVariableValueTo)_y).ValueToFloat());

        public void AddForce(Vector2Variable _v) => myExtendedComponent.AddForce(_v, ForceMode2D.Force);
        public void AddForceX(ChangeableVariable _x) => myExtendedComponent.AddForce(new Vector2(((IVariableValueTo)_x).ValueToFloat(), 0), ForceMode2D.Force);
        public void AddForceY(ChangeableVariable _y) => myExtendedComponent.AddForce(new Vector2(0, ((IVariableValueTo)_y).ValueToFloat()), ForceMode2D.Force);

        public void AddImpulse(Vector2Variable _v) => myExtendedComponent.AddForce(_v, ForceMode2D.Impulse);
        public void AddImpulseX(ChangeableVariable _x) => myExtendedComponent.AddForce(new Vector2(((IVariableValueTo)_x).ValueToFloat(), 0), ForceMode2D.Impulse);
        public void AddImpulseY(ChangeableVariable _y) => myExtendedComponent.AddForce(new Vector2(0, ((IVariableValueTo)_y).ValueToFloat()), ForceMode2D.Impulse);

    }
}