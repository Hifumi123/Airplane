using System;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    //public Transform centerOfMass;

    public float airDensity;

    public float wingArea;

    public float wingLength;

    [NonSerialized]
    public Vector3 totalForce;

    [NonSerialized]
    public Vector3 totalTorque;

    //public Vector3 weight;

    public Vector3 thrust;

    [NonSerialized]
    public Vector3 lift;

    [NonSerialized]
    public float liftCoefficient;

    public float maxLiftCoefficient;

    public float minLiftCoefficient;

    public float liftCoefficientDelta;

    [NonSerialized]
    public Vector3 horizontalForce;

    [NonSerialized]
    public float horizontalForceValue;

    public float maxHorizontalForceValue;

    public float minHorizontalForceValue;

    [NonSerialized]
    public Vector3 drag;

    public float dragCoefficient;

    [NonSerialized]
    public Vector3 turnTorque;

    public float turnTorqueCoefficient;

    private Rigidbody m_Rigidbody;

    private bool m_InputJump;

    private bool m_EngineOn;

    private float m_InputHorizontal;

    private float m_InputVertical;

    private bool m_InputQ;

    private bool m_InputE;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_Rigidbody.centerOfMass = centerOfMass.position;
    }

    void Update()
    {
        m_InputJump = Input.GetButtonDown("Jump");
        m_InputHorizontal = Input.GetAxis("Horizontal");
        m_InputVertical = Input.GetAxis("Vertical");
        m_InputQ = Input.GetKey(KeyCode.Q);
        m_InputE = Input.GetKey(KeyCode.E);

        if (m_InputJump)
        {
            if (m_EngineOn)
            {
                m_EngineOn = false;
            }
            else
            {
                m_EngineOn = true;
            }

            m_InputJump = false;
        }

        if (!Mathf.Approximately(m_InputHorizontal, 0))
        {
            if (m_InputHorizontal > 0)
            {
                horizontalForceValue = maxHorizontalForceValue;
            }
            else
            {
                horizontalForceValue = minHorizontalForceValue;
            }
        }
        else
        {
            horizontalForceValue = 0;
        }

        if (!Mathf.Approximately(m_InputVertical, 0))
        {
            if (m_InputVertical > 0)
            {
                if (liftCoefficient < maxLiftCoefficient)
                {
                    liftCoefficient += liftCoefficientDelta;
                }
            }
            else
            {
                if (liftCoefficient > minLiftCoefficient)
                {
                    liftCoefficient -= liftCoefficientDelta;
                }
            }
        }

        if (m_InputQ)
        {
            if (!m_InputE)
            {
                turnTorqueCoefficient = -1;
            }
            else
            {
                if (turnTorqueCoefficient != 0)
                {
                    turnTorqueCoefficient = 0;
                }
            }
        }
        else if (m_InputE)
        {
            turnTorqueCoefficient = 1;
        }
        else
        {
            if (turnTorqueCoefficient != 0)
            {
                turnTorqueCoefficient = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        totalForce = new Vector3(0, 0, 0);
        totalTorque = new Vector3(0, 0, 0);

        Vector3 velocity = m_Rigidbody.velocity;

        //totalForce += weight;

        if (m_EngineOn)
        {
            totalForce += thrust;
        }

        float tempZ = airDensity * velocity.z * velocity.z * wingArea / 2;
        float tempX = airDensity * velocity.x * velocity.x * wingArea / 2;

        lift = new Vector3(0, 0, 0);
        lift.y = liftCoefficient * tempZ;

        totalForce += lift;

        horizontalForce = new Vector3(0, 0, 0);
        horizontalForce.x = horizontalForceValue;

        totalForce += horizontalForce;

        drag = new Vector3(0, 0, 0);
        drag.z = dragCoefficient * tempZ;
        drag.z = -drag.z;
        drag.x = dragCoefficient * tempX;
        if (velocity.x > 0)
        {
            drag.x = -drag.x;
        }

        totalForce += drag;

        turnTorque = new Vector3(0, 0, 0);
        turnTorque.y = turnTorqueCoefficient * tempZ * wingLength;

        totalTorque += turnTorque;

        m_Rigidbody.AddForce(totalForce);
        m_Rigidbody.AddTorque(totalTorque);
    }
}
