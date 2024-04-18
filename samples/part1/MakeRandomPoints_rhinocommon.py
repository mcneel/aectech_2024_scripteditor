#! python3
# requirements: numpy

import Rhino
import Rhino.DocObjects
import numpy as np
import System 

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
    for i in range(num_points):
        point = Rhino.Geometry.Point3d(x_coords[i], y_coords[i], z_coords[i])
        Rhino.RhinoDoc.ActiveDoc.Objects.AddPoint(point)

if __name__ == "__main__":
    create_random_points()