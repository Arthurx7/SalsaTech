using UnityEngine;
using System.Collections.Generic;

public class SalsaMaster : MonoBehaviour
{
    // References
    public AudioSource audioSource;
    public List<AudioClip> songList;
    public List<Material> materials;
    public List<Material> skyboxes;

    public GameObject maleDancer;
    public GameObject femaleDancerObject1;
    public GameObject femaleDancerObject2;

    public Animator maleAnimator;
    public Animator femaleAnimator;

    private int currentSongIndex = 0;
    private int currentSkyboxIndex = 0;

    private void Start()
    {
        // Start with the first song and skybox
        PlayCurrentSong();
        SetCurrentSkybox();
    }

    private void PlayCurrentSong()
    {
        if (songList.Count == 0)
        {
            Debug.LogWarning("Song list is empty!");
            return;
        }

        audioSource.clip = songList[currentSongIndex];
        audioSource.Play();
    }

    private void PlayNextSong()
    {
        if (songList.Count == 0)
        {
            Debug.LogWarning("Song list is empty!");
            return;
        }

        currentSongIndex = (currentSongIndex + 1) % songList.Count;
        PlayCurrentSong();
    }

    private void SetCurrentSkybox()
    {
        if (skyboxes.Count == 0)
        {
            Debug.LogWarning("Skybox list is empty!");
            return;
        }

        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
    }

    private void ChangeSkybox()
    {
        if (skyboxes.Count == 0)
        {
            Debug.LogWarning("Skybox list is empty!");
            return;
        }

        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Count;
        SetCurrentSkybox();
    }

    public void ChangeToCamisetaAmarilla()
    {
        ChangeMaleMaterial(0);
        PlayMaleAnimation("SalsaDancing");
    }

    public void ChangeToCamisetaAzul()
    {
        ChangeMaleMaterial(1);
        PlayMaleAnimation("SalsaDancing0");
    }

    public void ChangeToCamisetaBlanca()
    {
        ChangeMaleMaterial(2);
        PlayMaleAnimation("SalsaDancing1");
    }

    public void ChangeToCamisetaRoja()
    {
        ChangeMaleMaterial(3);
        PlayMaleAnimation("SalsaDancing");
    }

    public void ChangeToVestidoRojo()
    {
        ChangeFemaleMaterial(4);
        PlayFemaleAnimation("SalsaDancing2");
    }

    public void ChangeToVestidoAzul()
    {
        ChangeFemaleMaterial(5);
        PlayFemaleAnimation("SalsaDancing3");
    }

    public void ChangeToVestidoAmarillo()
    {
        ChangeFemaleMaterial(6);
        PlayFemaleAnimation("SalsaDancing4");
    }

    public void ChangeToVestidoBlanco()
    {
        ChangeFemaleMaterial(7);
        PlayFemaleAnimation("SalsaDancing2");
    }

    private void ChangeMaleMaterial(int materialIndex)
    {
        if (materials.Count <= materialIndex)
        {
            Debug.LogWarning($"Material index {materialIndex} is out of range!");
            return;
        }

        Renderer maleRenderer = maleDancer.GetComponent<Renderer>();
        if (maleRenderer != null)
        {
            maleRenderer.material = materials[materialIndex];
        }

        UpdateScene();
    }

    private void ChangeFemaleMaterial(int materialIndex)
    {
        if (materials.Count <= materialIndex)
        {
            Debug.LogWarning($"Material index {materialIndex} is out of range!");
            return;
        }

        Renderer femaleRenderer1 = femaleDancerObject1.GetComponent<Renderer>();
        Renderer femaleRenderer2 = femaleDancerObject2.GetComponent<Renderer>();

        if (femaleRenderer1 != null)
        {
            femaleRenderer1.material = materials[materialIndex];
        }

        if (femaleRenderer2 != null)
        {
            femaleRenderer2.material = materials[materialIndex];
        }

        UpdateScene();
    }

    private void UpdateScene()
    {
        PlayNextSong();
        ChangeSkybox();
    }

     private void PlayMaleAnimation(string animationName)
    {
        if (maleAnimator != null)
        {
            maleAnimator.Play(animationName);
        }
    }

    private void PlayFemaleAnimation(string animationName)
    {
        if (femaleAnimator != null)
        {
            femaleAnimator.Play(animationName);
        }
    }
}
