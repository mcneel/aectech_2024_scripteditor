#! python3

# r: numpy

import numpy as np 
import rhinoscriptsyntax as rs

def create_random_points():

    min_value = rs.GetReal("Enter min bound", 0, 0)
    max_value = rs.GetReal("Enter max bound", 10, min_value)

    if min_value == None or max_value == None:
        return;

    num_points = 50
    x_coord = np.random.uniform(min_value, max_value, num_points)
    y_coord = np.random.uniform(min_value, max_value, num_points)
    z_coord = np.random.uniform(min_value, max_value, num_points)

    for i in range(50):
        rs.AddPoint(x_coord[i], y_coord[i], z_coord[i])
    
if __name__ == "__main__":
    create_random_points()
