#version 330 core

uniform vec3 lightColor;
uniform vec3 thisColor;
in vec3 Normal;
in vec3 FragPos; 
uniform vec3 lightPos;
out vec4 color;
struct Material{
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};
struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;
uniform Material material;
void main()
{
    vec3 ambient = material.ambient * lightColor;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff*material.diffuse * lightColor;

    vec3 viewDir = normalize(-FragPos);
    vec3 reflectDir = reflect(-lightDir, Normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular =  material.specular*  spec; 
    color = vec4((light.ambient*ambient + light.diffuse*diffuse +light.specular*specular) ,1.0f); 
}