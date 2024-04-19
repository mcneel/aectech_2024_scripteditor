#! python3
# requirements: numpy, scikit-learn

import Rhino
import Rhino.DocObjects
import numpy as np
import System 
from sklearn.cluster import KMeans

def create_random_points():
    # Ask the user for minimum and maximum limits
    min_limit = 0.0
    max_limit = 10
    sc1, min_limit = Rhino.Input.RhinoGet.GetNumber("Enter the min bound", False, min_limit)
    sc1, max_limit = Rhino.Input.RhinoGet.GetNumber("Enter the max bound", False, max_limit)

    print(f"Min limit {min_limit}")
    print(f"Max limit {max_limit}")

    # # Generate random coordinates within the given bounds
    num_points = 100  # You can change this to the desired number of points
    x_coords = np.random.uniform(min_limit, max_limit, num_points)
    y_coords = np.random.uniform(min_limit, max_limit, num_points)
    z_coords = np.random.uniform(min_limit, max_limit, num_points)

    # Create Point3d objects and add them to Rhino
    points = []
    for i in range(num_points):
        point = Rhino.Geometry.Point3d(x_coords[i], y_coords[i], z_coords[i])
        points.append(point)
    
    # for p in points:
    #     Rhino.RhinoDoc.ActiveDoc.Objects.AddPoint(p)

    # Convert points to numpy array for clustering
    data = np.array([[point.X, point.Y, point.Z] for point in points])

    # Perform K-means clustering
    num_clusters = 3
    kmeans = KMeans(num_clusters)
    kmeans.fit(data)
    centroids = kmeans.cluster_centers_
    labels = kmeans.labels_

    for i in range(num_clusters):
        color = [0, 0, 0]
        color[i] = 255
        layer_name = f"Cluster {i+1}"
        layer_color = System.Drawing.Color.FromArgb(255, color[0], color[1], color[2])
        layer_index = Rhino.RhinoDoc.ActiveDoc.Layers.Add(layer_name, layer_color)
        print(f"Layer index {layer_name}: {layer_index}")

        cluster_points = []
        for j in range(labels):
            if labels[j] == i:
                cluster_points.append(points[j])
        print(f"Points in cluster {i + 1}: {len(cluster_points)}")

        object_attributes = Rhino.DocObjects.ObjectAttributes()
        object_attributes.LayerIndex = Rhino.RhinoDoc.ActiveDoc.Layers.FindName(layer_name).LayerIndex

        for p in cluster_points:
            Rhino.RhinoDoc.ActiveDoc.Objects.AddPoint(p, object_attributes)

    # Redraw Rhino viewport
    Rhino.RhinoDoc.ActiveDoc.Views.Redraw()

if __name__ == "__main__":
    create_random_points()