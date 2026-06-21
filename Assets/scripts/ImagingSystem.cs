using UnityEngine;

public class ImagingSystem : MonoBehaviour
{
    public Transform realObject;       // Asli cylinder (Object)
    public Transform invertedImage;    // Screen par ulta cylinder (Image)

    public float focalLength = 2.0f;   // Lens ki focal length (f)
    public float lensZPosition = 3.0f; // Lens ki position (Z-axis par)

    void Update()
    {
        if (realObject == null || invertedImage == null) return;

        // 1. Object ka lens se distance (do) maloom karna
        float doDistance = Mathf.Abs(lensZPosition - realObject.position.z);

        // Physics Guard: Agar object focal point ke andar aa jaye toh screen par image nahi banti
        if (doDistance <= focalLength)
        {
            invertedImage.gameObject.SetActive(false);
            return;
        }

        invertedImage.gameObject.SetActive(true);

        // 2. Lens Formula: 1/di = 1/f - 1/do  => Is se Image distance (di) niklega
        float diDistance = 1.0f / ((1.0f / focalLength) - (1.0f / doDistance));

        // 3. Magnification (M) = di / do (Size kitna bada ya chota hoga)
        float magnification = diDistance / doDistance;

        // 4. Image ka size (Scale) automatic change karna
        float newScaleY = 0.4f * magnification; 
        float newScaleX = 0.2f * magnification;

        invertedImage.localScale = new Vector3(newScaleX, newScaleY, newScaleX);
    }
}