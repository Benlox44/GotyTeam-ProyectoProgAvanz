using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionNivel : MonoBehaviour
{
    public void CambiarNivel(int numeroNivel){
        SceneManager.LoadScene(numeroNivel);       
    }
}
