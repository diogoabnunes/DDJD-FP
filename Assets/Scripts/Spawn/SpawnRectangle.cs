using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRectangle {
    Vector3 centerPoint;
    
    float width;
    float height;

    float rotation;

    public SpawnRectangle(Vector3 centerPoint, float width, float height, float rotation) {
        this.centerPoint = centerPoint;
        this.width = width;
        this.height = height;
        this.rotation = rotation;
    }

    public float getMinXPoint() {
        return centerPoint.x - (width / 2f);
    }

    public float getMaxXPoint() {
        return centerPoint.x + (width / 2f);
    }

    public float getMinZPoint() {
        return centerPoint.z - (height / 2f);
    }

    public float getMaxZPoint() {
        return centerPoint.z + (height / 2f);
    }

    public float getY() {
        return centerPoint.y;
    }

    public Vector3 rotatePointToRectangleRotation(Vector3 point) {
        // float distanceToCenter = Mathf.Sqrt(Mathf.Pow(centerPoint.x - point.x, 2) + Mathf.Pow(centerPoint.z - point.z, 2));
        // float newX = distanceToCenter * Mathf.Cos(rotation * Mathf.Deg2Rad) + centerPoint.x;
        // float newZ = distanceToCenter * Mathf.Sin(rotation * Mathf.Deg2Rad) + centerPoint.z;

        // return new Vector3(newX, point.y, newZ);
        // float newX = point.x * Mathf.Cos(rotation * Mathf.Deg2Rad)
        return point;
    }
}