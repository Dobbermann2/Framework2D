#version 460 core

layout(location = 0) in vec3 aPosition;

layout(location = 1) in vec2 aTexCoord;

layout(location = 2) in int aTexSlot;
out vec2 texCoord;
flat out int texSlot;

uniform mat4 projection;

void main(void)
{
    texCoord = aTexCoord;
    texSlot = aTexSlot;
    gl_Position = projection*vec4(aPosition, 1.0);
}