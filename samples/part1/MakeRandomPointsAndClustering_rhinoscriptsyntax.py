# requirements: numpy

import rhinoscriptsyntax as rs
import numpy as np
from sklearn.cluster import KMeans

def create_random_points():
    # Ask the user for minimum and maximum limits
    min_limit = rs.GetReal("Enter the minimum limit:", minimum=0)
    max_limit = rs.GetReal("Enter the maximum limit:", minimum=min_limit)
    
    if min_limit is None or max_limit is None:
        print("Invalid input. Aborting.")
        return

    # Generate random coordinates within the given bounds
    num_points = 50  # You can change this to the desired number of points
    x_coords = np.random.uniform(min_limit, max_limit, num_points)
    y_coords = np.random.uniform(min_limit, max_limit, num_points)
    z_coords = np.random.uniform(min_limit, max_limit, num_points)

    # Create Point3d objects and add them to Rhino
    points = []
    for i in range(num_points):
        point = rs.CreatePoint(x_coords[i], y_coords[i], z_coords[i])
        points.append(point)
    
    # Convert points to numpy array for clustering
    data = np.array([[point.X, point.Y, point.Z] for point in points])

    # Perform K-means clustering
    num_clusters = 3  # You can change this to the desired number of clusters
    kmeans = KMeans(n_clusters=num_clusters)
    kmeans.fit(data)
    centroids = kmeans.cluster_centers_
    labels = kmeans.labels_

    # Create layers for each cluster and move points to the layers
    for i in range(num_clusters):
        layer_name = "Cluster_" + str(i + 1)
        rs.AddLayer(layer_name)
        color = [255, 0, 0] if i == 0 else [0, 255, 0] if i == 1 else [0, 0, 255]
        rs.LayerColor(layer_name, color)
        
        # Move points to their corresponding layers
        cluster_points = [points[j] for j in range(len(points)) if labels[j] == i]
        for point in cluster_points:
            point_id = rs.coerceguid(rs.AddPoint(point))
            rs.ObjectLayer(point_id, layer_name)

if __name__ == "__main__":
    create_random_points()