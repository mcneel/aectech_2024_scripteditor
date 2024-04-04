#! python 3
# requirements: numpy

import Rhino
import Rhino.Input
import rhinoscriptsyntax as rs
import scriptcontext as sc
import numpy as np

def create_random_points():
    # Ask the user for minimum and maximum limits
    min_limit = rs.GetReal("Enter the minimum limit:", minimum=0)
    max_limit = rs.GetReal("Enter the maximum limit:", minimum=min_limit)

    if min_limit is None or max_limit is None:
        print("Invalid input. Aborting.")
        return

    # Generate random coordinates within the given bounds
    num_points = 10  # You can change this to the desired number of points
    x_coords = np.random.uniform(min_limit, max_limit, num_points)
    print(x_coords)
    y_coords = np.random.uniform(min_limit, max_limit, num_points)
    print(y_coords)
    z_coords = np.random.uniform(min_limit, max_limit, num_points)
    print(z_coords)

    # Create Point3d objects and add them to Rhino
    points = []
    for i in range(num_points):
        point = Rhino.Geometry.Point3d(x_coords[i], y_coords[i], z_coords[i])
        points.append(point)
    
    # Add points to Rhino document
    for point in points:
        sc.doc.Objects.AddPoint(point)

    # Redraw Rhino viewport
    sc.doc.Views.Redraw()

if __name__ == "__main__":
    create_random_points()