#version 460 core

layout(location = 0) in vec3 aPosition;

layout(location = 1) in vec2 aTexCoord;

layout(location = 2) in float aTexSlot;

out vec2 texCoord;

out float texSlot;

uniform mat4 projection;
uniform mat4 view;

void main(void)
{
    texCoord = aTexCoord;
    texSlot = aTexSlot;
    gl_Position = projection*view*vec4(aPosition, 1.0);
}