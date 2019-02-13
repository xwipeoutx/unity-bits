# Unity Bits

This repository contains snippets and examples of helpful effects and scripts I've used.

# Shaders

## Sweep Reveal

Simple effect to transition between alpha=0 and alpha=1 with a sweeping blend.  Good for revealing panels or terrain layers, for example.

## Occlusion only

Shader that writes to the depth buffer, but does not render any colour.  Useful in AR when you have a mesh of the "real world" and wish to ensure holograms "hide behind" them.

Note the Render Queue is `Geometry-1` to ensure it renders first - or there isn't anything to occlude!

## Lighting

Sometimes you just want to copy/paste some standard lighting into your own shader.  This is a set of shaders that build upon each other to get good enough lighting in most cases.

Includes

- Ambient (flat colour of world)
- Lambert (diffuse)
- Blinn-phong (specular)
- Fresnel (rim lighting)

# Behaviours

## Transform Tween

Tweens a game object between 2 other transforms.  Useful for moving panels around in space as interaction occurs.  Applies position, rotation and scale

## Timeline Orchestrator

Manages transitions between complex states via timelines.  This is just an example pattern to follow.

# Gizmos

A bunch of Gizmos that I'm not gonna spend much effort documenting

**FrustumPreview:** Good for vantage points and alternate camera locations - shows a box and a frustum that's pointing the right way
**PathPreview:** If you're doing a custom path and want a preview, this is your gizmo - shows a "tadpole" at a bunch of samples along a path