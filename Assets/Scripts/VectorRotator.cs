//using UnityEngine;


//public class VectorRotator : MonoBehaviour
//{
//    [HideInInspector]
//    public Vector2 vector = Vector2.zero;// = Quaternion.Euler(0f, 0f, 90f) * Vector2.right;
//    [HideInInspector]
//    public bool start = false;
    
//    public Transform myTransform;

//    private float timeElapsed = 0f;
//    private float rotationProgress = 0f;
//    private const float rotationTime = 1.3f; // 0.5 segundo

//    private void Start()
//    {
//        //myTransform = this.transform;
//    }

//    void Update()
//    {
//        if (start)
//        {
//            timeElapsed += Time.deltaTime;
//            if (timeElapsed >= rotationTime)
//            {
//                timeElapsed = 0f;
//                rotationProgress = 0f;
//            }

//            // Calcular el �ngulo de rotaci�n para este frame
//            float rotationAngle = 360f * Time.deltaTime / rotationTime;

//            // Incrementar el progreso de rotaci�n
//            rotationProgress += rotationAngle;

//            // Aplicar la rotaci�n al vector
//            vector = Quaternion.Euler(0f, 0f, rotationAngle) * vector;

//            //// Mover el objeto en la direcci�n del vector
//            //transform.rotation = Quaternion.Euler(45, 0, vector.z);
            
//        }

//        // Rotar el parent para que su vector.right sea igual al vector de direcci�n
//        transform.parent.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)vector);

//    }

//    public void Restart()
//    {
//        vector = Vector2.zero;// = Quaternion.Euler(0f, 0f, 90f) * Vector2.right;
//        start = false;
//        timeElapsed = 0f;
//        rotationProgress = 0f;
//    }
      
//}
