# Unity Bits

This repository contains snippets and examples of helpful effects and scripts I've used.

## Sweep Reveal

Simple effect to transition between alpha=0 and alpha=1 with a sweeping blend.  Good for revealing panels or terrain layers, for example.

Shader can be found at `UnityHelpers/Assets/Shaders/Sweep/SweepReveal.shader`

See `UnityHelpers/Assets/Shaders/Sweep/Example` for an example.

## Occlusion only

Shader that writes to the depth buffer, but does not render any colour.  Useful in AR when you have a mesh of the "real world" and wish to ensure holograms "hide behind" them.

Note the Render Queue is `Geometry-1` to ensure it renders first - or there isn't anything to occlude!

Shader can be found at `UnityHelpers/Assets/Shaders/Occlusion/Occlusion Only.shader`

See `UnityHelpers/Assets/Shaders/Occlusion/Example` for an example.

## Lighting

Sometimes you just want to copy/paste some standard lighting into your own shader.  This is a set of shaders that build upon each other to get good enough lighting in most cases.

Includes

- Ambient (flat colour of world)
- Lambert (diffuse)
- Blinn-phong (specular)
- Fresnel (rim lighting)

Shaders can be found at `UnityHelpers/Assets/Shaders/Lighting/`

See `UnityHelpers/Assets/Shaders/Lighting/Example` for an example.