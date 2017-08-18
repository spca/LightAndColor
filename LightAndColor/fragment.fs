#version 330 core

uniform vec3 lightColor;
uniform vec3 thisColor;
out vec4 color;
void main()
{
    color = vec4(lightColor*thisColor,1.0f); 
}