// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace World_Function
// {
//     public class DistanceCulling : MonoBehaviour
//     {
//         public float cullingDistance = 50f; // Distance to deactivate objects
//         public string[] targetTags = new string[] { "Tree", "Rock" }; // Tags for objects to cull
//
//         private List<Transform> _targets = new List<Transform>(); // Updated naming convention
//
//         private void Start()
//         {
//             // Gather all target objects using the specified tags
//             foreach (var targetTag in targetTags) // Renamed local variable to avoid conflict
//             {
//                 GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag); // No ambiguity
//                 foreach (GameObject obj in objects)
//                 {
//                     if (obj != null && obj.transform != null)
//                     {
//                         _targets.Add(obj.transform); // Add transforms of valid objects
//                     }
//                 }
//             }
//
//             // Start the culling coroutine
//             StartCoroutine(CullObjectsCoroutine());
//         }
//
//         private IEnumerator CullObjectsCoroutine()
//         {
//             while (true)
//             {
//                 List<int> nullIndices = new List<int>();
//
//                 // Cull objects in small batches to avoid frame lag
//                 for (int i = _targets.Count - 1; i >= 0; i--) 
//                 {
//                     Transform target = _targets[i];
//
//                     if (target == null) // Collect indices to remove later
//                     {
//                         nullIndices.Add(i);
//                         continue;
//                     }
//
//                     // Ensure object exists before checking distance
//                     if (target != null) 
//                     {
//                         float distance = Vector3.Distance(transform.position, target.position);
//                         bool withinRange = distance <= cullingDistance;
//                 
//                         if (target.gameObject.activeSelf != withinRange)
//                         {
//                             target.gameObject.SetActive(withinRange);
//                         }
//                     }
//
//                     // Spread processing across frames
//                     if (i % 10 == 0) 
//                     {
//                         yield return null;
//                     }
//                 }
//
//                 // Remove null targets in bulk to avoid modifying list mid-loop
//                 foreach (int index in nullIndices)
//                 {
//                     _targets.RemoveAt(index);
//                 }
//
//                 // Ensure coroutine always yields
//                 yield return new WaitForSeconds(0.5f);
//             }
//         }
//     }
// }

// FIXME:: Disable until required