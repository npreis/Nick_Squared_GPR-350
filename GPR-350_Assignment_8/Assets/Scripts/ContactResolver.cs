using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ContactResolver : MonoBehaviour
{
    int mIterations;
    int mIterationsUsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetIteration(int iterations)
    {
        mIterations = iterations;
    }

    void ResolveContacts(List<Particle2DContact> contacts, double dt)
    {
		mIterationsUsed = 0;
		while (mIterationsUsed < mIterations)
		{
			float max = float.MaxValue;
			int numContacts = contacts.Count;
			int maxIndex = numContacts;
			for (int i = 0; i < numContacts; i++)
			{
				float sepVel = contacts[i].CalculateSeparatingVelocity();
				if (sepVel < max && (sepVel < 0.0f || contacts[i].mPenetration > 0.0f))
				{
					max = sepVel;
					maxIndex = i;
				}
			}
			if (maxIndex == numContacts)
				break;

			contacts[maxIndex].Resolve();

			for (int i = 0; i < numContacts; i++)
			{
				if (contacts[i].mObj1 == contacts[maxIndex].mObj1)
				{
					contacts[i].mPenetration -= contacts[maxIndex].mMove1.Dot(contacts[i].mContactNormal);
				}
				else if (contacts[i].mObj1 == contacts[maxIndex].mObj2)
				{
					contacts[i].mPenetration -= contacts[maxIndex].mMove2.Dot(contacts[i].mContactNormal);
				}

				if (contacts[i].mObj2)
				{
					if (contacts[i].mObj2 == contacts[maxIndex].mObj1)
					{
						contacts[i].mPenetration += contacts[maxIndex].mMove1.Dot(contacts[i].mContactNormal);
					}
					else if (contacts[i].mObj2 == contacts[maxIndex].mObj2)
					{
						contacts[i].mPenetration -= contacts[maxIndex].mMove2.Dot(contacts[i].mContactNormal);
					}
				}
			}
			mIterationsUsed++;
		}
	}
}
