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
        return centerPoint.x - (height / 2f);
    }

    public float getMaxXPoint() {
        return centerPoint.x + (height / 2f);
    }

    public float getMinZPoint() {
        return centerPoint.z - (width / 2f);
    }

    public float getMaxZPoint() {
        return centerPoint.z + (width / 2f);
    }

    public float getY() {
        return centerPoint.y;
    }

    public Vector3 rotatePointToRectangleRotation(Vector3 point) {
        Vector3 vector1 = new Vector3(0, 0, centerPoint.z + (width / 2f));
        Vector3 vector2 = new Vector3(point.x - centerPoint.x, point.y - centerPoint.y, point.z - centerPoint.z);
        float angle = Vector3.Angle(vector1, vector2) + rotation;

        float radius = Mathf.Sqrt(Mathf.Pow(vector2.x, 2) + Mathf.Pow(vector2.z, 2));

        float newX = radius * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPoint.x;
        float newZ = radius * Mathf.Cos(angle * Mathf.Deg2Rad) + centerPoint.z;

        return new Vector3(newX, point.y, newZ);
    }
}