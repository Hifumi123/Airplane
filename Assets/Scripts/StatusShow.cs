using UnityEngine;
using UnityEngine.UI;

public class StatusShow : MonoBehaviour
{
    public Airplane airplane;

    public Text velocity;

    public Text liftCoefficient;

    public Text height;

    public float heightDelta;

    public Text totalForce;

    public Text lift;

    public Text drag;

    public Text totalTorque;

    void Start()
    {
        
    }

    void Update()
    {
        Rigidbody airplaneRigidbody = airplane.GetComponent<Rigidbody>();

        float velocityXValue = airplaneRigidbody.velocity.x;
        float velocityYValue = airplaneRigidbody.velocity.y;
        float velocityZValue = airplaneRigidbody.velocity.z;
        velocityXValue = Utils.Remian2Decimals(velocityXValue);
        velocityYValue = Utils.Remian2Decimals(velocityYValue);
        velocityZValue = Utils.Remian2Decimals(velocityZValue);
        velocity.text = "速度：(" + velocityXValue + ", " + velocityYValue + ", " + velocityZValue + ") m/s";

        float liftCoefficientValue = airplane.liftCoefficient;
        liftCoefficientValue = Utils.Remian2Decimals(liftCoefficientValue);
        liftCoefficient.text = "升力系数：" + liftCoefficientValue;

        //float heightValue = airplane.transform.position.y + heightDelta;
        float heightValue = airplane.transform.position.y;
        heightValue = Utils.Remian2Decimals(heightValue);
        height.text = "高度：" + heightValue + " m";

        float totalForceXValue = airplane.totalForce.x;
        float totalForceYValue = airplane.totalForce.y + Physics.gravity.y * airplaneRigidbody.mass;
        float totalForceZValue = airplane.totalForce.z;
        totalForceXValue = Utils.Remian2Decimals(totalForceXValue);
        totalForceYValue = Utils.Remian2Decimals(totalForceYValue);
        totalForceZValue = Utils.Remian2Decimals(totalForceZValue);
        totalForce.text = "总受力：(" + totalForceXValue + ", " + totalForceYValue + ", " + totalForceZValue + ") N";

        float liftValue = airplane.lift.y;
        liftValue = Utils.Remian2Decimals(liftValue);
        lift.text = "升力：" + liftValue + " N";

        float dragXValue = airplane.drag.x;
        float dragYValue = airplane.drag.y;
        float dragZValue = airplane.drag.z;
        dragXValue = Utils.Remian2Decimals(dragXValue);
        dragYValue = Utils.Remian2Decimals(dragYValue);
        dragZValue = Utils.Remian2Decimals(dragZValue);
        drag.text = "阻力：(" + dragXValue + ", " + dragYValue + ", " + dragZValue + ") N";

        float totalTorqueXValue = airplane.totalTorque.x;
        float totalTorqueYValue = airplane.totalTorque.y;
        float totalTorqueZValue = airplane.totalTorque.z;
        totalTorqueXValue = Utils.Remian2Decimals(totalTorqueXValue);
        totalTorqueYValue = Utils.Remian2Decimals(totalTorqueYValue);
        totalTorqueZValue = Utils.Remian2Decimals(totalTorqueZValue);
        totalTorque.text = "总力矩：(" + totalTorqueXValue + ", " + totalTorqueYValue + ", " + totalTorqueZValue + ") N·m";
    }
}
