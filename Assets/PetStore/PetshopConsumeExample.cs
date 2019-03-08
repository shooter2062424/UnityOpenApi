﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOpenApi;
using PetStore;
using Newtonsoft.Json;

public class PetshopConsumeExample : MonoBehaviour
{
    [Header("Get Pets")]
    [SerializeField] PathItemAsset pets = null;
    [SerializeField] string tags = "cat";
    [SerializeField] int limit = 3;

    [Header("Get Single Pet")]
    [SerializeField] PathItemAsset pet = null;
    [SerializeField] string petIdToGet = "5";

    [Header("New pet")]
    [SerializeField] NewPet newPet = null;

    [ContextMenu("Get Pets")]
    public void GetPets()
    {
        var operation = pets.GetOperation(AOOperationType.Get);
        operation.SetParameterValue("tags", tags);
        operation.SetParameterValue("limit", limit.ToString());

        pets.ExecuteOperation<List<Pet>>(operation, pets =>
        {
            pets.ForEach(pet =>
            {
                Debug.Log("Pet ID: " + pet.id + ", type: " + pet.type + ", price: " + pet.price);
            });
        });
    }

    [ContextMenu("Get Single Pet")]
    public void GetPet()
    {
        var operation = pet.GetOperation(AOOperationType.Get);
        operation.SetParameterValue("id", petIdToGet);

        pet.ExecuteOperation<Pet>(operation, pet =>
        {
            Debug.Log("Pet ID: " + pet.id + ", type: " + pet.type + ", price: " + pet.price);
        });
    }

    [ContextMenu("Create Pet")]
    public void CreatePet()
    {
        var operation = pets.GetOperation(AOOperationType.Post);
        var serialized = JsonConvert.SerializeObject(newPet);
        operation.SetRequestBody(serialized);
        
        pets.ExecuteOperation<NewPetResponse>(operation, r =>
        {
            var pet = r.pet;
            Debug.Log("Pet name: " + pet.name + ", type: " + pet.type + ", price: " + pet.price);
        });
    }

    [ContextMenu("Test Hash")]
    void TestHash()
    {
        var operation = pets.GetOperation(AOOperationType.Post);
        Debug.Log(operation.OperationCurrentHash);
    }
}
