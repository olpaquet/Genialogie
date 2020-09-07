#!/usr/bin/env python
# encoding: utf-8
# Généré par Mocodo 2.3.7 le Sun, 06 Sep 2020 23:09:25

from __future__ import division
from math import hypot

import time, codecs

import json

with codecs.open('DBGenialogie_toutsaufblabla_geo.json') as f:
    geo = json.loads(f.read())
(width,height) = geo.pop('size')
for (name, l) in geo.items(): globals()[name] = dict(l)
card_max_width = 22
card_max_height = 15
card_margin = 5
arrow_width = 12
arrow_half_height = 6
arrow_axis = 8
card_baseline = 3

def cmp(x, y):
    return (x > y) - (x < y)

def offset(x, y):
    return (x + card_margin, y - card_baseline - card_margin)

def line_intersection(ex, ey, w, h, ax, ay):
    if ax == ex:
        return (ax, ey + cmp(ay, ey) * h)
    if ay == ey:
        return (ex + cmp(ax, ex) * w, ay)
    x = ex + cmp(ax, ex) * w
    y = ey + (ay-ey) * (x-ex) / (ax-ex)
    if abs(y-ey) > h:
        y = ey + cmp(ay, ey) * h
        x = ex + (ax-ex) * (y-ey) / (ay-ey)
    return (x, y)

def straight_leg_factory(ex, ey, ew, eh, ax, ay, aw, ah, cw, ch):
    
    def card_pos(twist, shift):
        compare = (lambda x1_y1: x1_y1[0] < x1_y1[1]) if twist else (lambda x1_y1: x1_y1[0] <= x1_y1[1])
        diagonal = hypot(ax-ex, ay-ey)
        correction = card_margin * 1.4142 * (1 - abs(abs(ax-ex) - abs(ay-ey)) / diagonal) - shift
        (xg, yg) = line_intersection(ex, ey, ew, eh + ch, ax, ay)
        (xb, yb) = line_intersection(ex, ey, ew + cw, eh, ax, ay)
        if compare((xg, xb)):
            if compare((xg, ex)):
                if compare((yb, ey)):
                    return (xb - correction, yb)
                return (xb - correction, yb + ch)
            if compare((yb, ey)):
                return (xg, yg + ch - correction)
            return (xg, yg + correction)
        if compare((xb, ex)):
            if compare((yb, ey)):
                return (xg - cw, yg + ch - correction)
            return (xg - cw, yg + correction)
        if compare((yb, ey)):
            return (xb - cw + correction, yb)
        return (xb - cw + correction, yb + ch)
    
    def arrow_pos(direction, ratio):
        (x0, y0) = line_intersection(ex, ey, ew, eh, ax, ay)
        (x1, y1) = line_intersection(ax, ay, aw, ah, ex, ey)
        if direction == "<":
            (x0, y0, x1, y1) = (x1, y1, x0, y0)
        (x, y) = (ratio * x0 + (1 - ratio) * x1, ratio * y0 + (1 - ratio) * y1)
        return (x, y, x1 - x0, y0 - y1)
    
    straight_leg_factory.card_pos = card_pos
    straight_leg_factory.arrow_pos = arrow_pos
    return straight_leg_factory


def curved_leg_factory(ex, ey, ew, eh, ax, ay, aw, ah, cw, ch, spin):
    
    def bisection(predicate):
        (a, b) = (0, 1)
        while abs(b - a) > 0.0001:
            m = (a + b) / 2
            if predicate(bezier(m)):
                a = m
            else:
                b = m
        return m
    
    def intersection(left, top, right, bottom):
       (x, y) = bezier(bisection(lambda p: left <= p[0] <= right and top <= p[1] <= bottom))
       return (int(round(x)), int(round(y)))
    
    def card_pos(shift):
        diagonal = hypot(ax-ex, ay-ey)
        correction = card_margin * 1.4142 * (1 - abs(abs(ax-ex) - abs(ay-ey)) / diagonal)
        (top, bot) = (ey - eh, ey + eh)
        (TOP, BOT) = (top - ch, bot + ch)
        (lef, rig) = (ex - ew, ex + ew)
        (LEF, RIG) = (lef - cw, rig + cw)
        (xr, yr) = intersection(LEF, TOP, RIG, BOT)
        (xg, yg) = intersection(lef, TOP, rig, BOT)
        (xb, yb) = intersection(LEF, top, RIG, bot)
        if spin > 0:
            if (yr == BOT and xr <= rig) or (xr == LEF and yr >= bot):
                return (max(x for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if y >= bot) - correction + shift, bot + ch)
            if (xr == RIG and yr >= top) or yr == BOT:
                return (rig, min(y for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if x >= rig) + correction + shift)
            if (yr == TOP and xr >= lef) or xr == RIG:
                return (min(x for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if y <= top) + correction + shift - cw, TOP + ch)
            return (LEF, max(y for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if x <= lef) - correction + shift + ch)
        if (yr == BOT and xr >= lef) or (xr == RIG and yr >= bot):
            return (min(x for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if y >= bot) + correction + shift - cw, bot + ch)
        if xr == RIG or (yr == TOP and xr >= rig):
            return (rig, max(y for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if x >= rig) - correction + shift + ch)
        if yr == TOP or (xr == LEF and yr <= top):
            return (max(x for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if y <= top) - correction + shift, TOP + ch)
        return (LEF, min(y for (x, y) in ((xr, yr), (xg, yg), (xb, yb)) if x <= lef) + correction + shift)
    
    def arrow_pos(direction, ratio):
        t0 = bisection(lambda p: abs(p[0] - ax) > aw or abs(p[1] - ay) > ah)
        t3 = bisection(lambda p: abs(p[0] - ex) < ew and abs(p[1] - ey) < eh)
        if direction == "<":
            (t0, t3) = (t3, t0)
        tc = t0 + (t3 - t0) * ratio
        (xc, yc) = bezier(tc)
        (x, y) = derivate(tc)
        if direction == "<":
            (x, y) = (-x, -y)
        return (xc, yc, x, -y)
    
    diagonal = hypot(ax - ex, ay - ey)
    (x, y) = line_intersection(ex, ey, ew + cw / 2, eh + ch / 2, ax, ay)
    k = (cw *  abs((ay - ey) / diagonal) + ch * abs((ax - ex) / diagonal))
    (x, y) = (x - spin * k * (ay - ey) / diagonal, y + spin * k * (ax - ex) / diagonal)
    (hx, hy) = (2 * x - (ex + ax) / 2, 2 * y - (ey + ay) / 2)
    (x1, y1) = (ex + (hx - ex) * 2 / 3, ey + (hy - ey) * 2 / 3)
    (x2, y2) = (ax + (hx - ax) * 2 / 3, ay + (hy - ay) * 2 / 3)
    (kax, kay) = (ex - 2 * hx + ax, ey - 2 * hy + ay)
    (kbx, kby) = (2 * hx - 2 * ex, 2 * hy - 2 * ey)
    bezier = lambda t: (kax*t*t + kbx*t + ex, kay*t*t + kby*t + ey)
    derivate = lambda t: (2*kax*t + kbx, 2*kay*t + kby)
    
    curved_leg_factory.points = (ex, ey, x1, y1, x2, y2, ax, ay)
    curved_leg_factory.card_pos = card_pos
    curved_leg_factory.arrow_pos = arrow_pos
    return curved_leg_factory


def upper_round_rect(x, y, w, h, r):
    return " ".join([str(x) for x in ["M", x + w - r, y, "a", r, r, 90, 0, 1, r, r, "V", y + h, "h", -w, "V", y + r, "a", r, r, 90, 0, 1, r, -r]])

def lower_round_rect(x, y, w, h, r):
    return " ".join([str(x) for x in ["M", x + w, y, "v", h - r, "a", r, r, 90, 0, 1, -r, r, "H", x + r, "a", r, r, 90, 0, 1, -r, -r, "V", y, "H", w]])

def arrow(x, y, a, b):
    c = hypot(a, b)
    (cos, sin) = (a / c, b / c)
    return " ".join([str(x) for x in [ "M", x, y, "L", x + arrow_width * cos - arrow_half_height * sin, y - arrow_half_height * cos - arrow_width * sin, "L", x + arrow_axis * cos, y - arrow_axis * sin, "L", x + arrow_width * cos + arrow_half_height * sin, y + arrow_half_height * cos - arrow_width * sin, "Z"]])

def safe_print_for_PHP(s):
    try:
        print(s)
    except UnicodeEncodeError:
        print(s.encode("utf8"))


lines = '<?xml version="1.0" standalone="no"?>\n<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN"\n"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">'
lines += '\n\n<svg width="%s" height="%s" view_box="0 0 %s %s"\nxmlns="http://www.w3.org/2000/svg"\nxmlns:link="http://www.w3.org/1999/xlink">' % (width,height,width,height)
lines += u'\\n\\n<desc>Généré par Mocodo 2.3.7 le %s</desc>' % time.strftime("%a, %d %b %Y %H:%M:%S", time.localtime())
lines += '\n\n<rect id="frame" x="0" y="0" width="%s" height="%s" fill="%s" stroke="none" stroke-width="0"/>' % (width,height,colors['background_color'] if colors['background_color'] else "none")

lines += u"""\n\n<!-- Entity Utilisateur -->"""
(x,y) = (cx[u"Utilisateur"],cy[u"Utilisateur"])
lines += u"""\n<g id="entity-Utilisateur">""" % {}
lines += u"""\n	<g id="frame-Utilisateur">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="112" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -56+x, 'y': -119+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="112" height="213" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -56+x, 'y': -94.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="112" height="238" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -56+x, 'y': -119+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -56+x, 'y0': -94+y, 'x1': 56+x, 'y1': -94+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">Utilisateur</text>""" % {'x': -31+x, 'y': -101.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">id</text>""" % {'x': -51+x, 'y': -76.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -51+x, 'y0': -74+y, 'x1': -39+x, 'y1': -74+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">login</text>""" % {'x': -51+x, 'y': -59.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nom</text>""" % {'x': -51+x, 'y': -42.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">prenom</text>""" % {'x': -51+x, 'y': -25.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">email</text>""" % {'x': -51+x, 'y': -8.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">datedenaissance</text>""" % {'x': -51+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">homme</text>""" % {'x': -51+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">motdepasse</text>""" % {'x': -51+x, 'y': 42.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">presel</text>""" % {'x': -51+x, 'y': 59.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">postsel</text>""" % {'x': -51+x, 'y': 76.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">cartedecredit</text>""" % {'x': -51+x, 'y': 93.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">actif</text>""" % {'x': -51+x, 'y': 110.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity Arbre -->"""
(x,y) = (cx[u"Arbre"],cy[u"Arbre"])
lines += u"""\n<g id="entity-Arbre">""" % {}
lines += u"""\n	<g id="frame-Arbre">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="86" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -43+x, 'y': -85+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="86" height="145" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -43+x, 'y': -60.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="86" height="170" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -43+x, 'y': -85+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -43+x, 'y0': -60+y, 'x1': 43+x, 'y1': -60+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">Arbre</text>""" % {'x': -17+x, 'y': -67.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">id</text>""" % {'x': -38+x, 'y': -42.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -38+x, 'y0': -40+y, 'x1': -26+x, 'y1': -40+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nom</text>""" % {'x': -38+x, 'y': -25.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">description</text>""" % {'x': -38+x, 'y': -8.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">createur</text>""" % {'x': -38+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">datecreation</text>""" % {'x': -38+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">blocage</text>""" % {'x': -38+x, 'y': 42.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">bloqueur</text>""" % {'x': -38+x, 'y': 59.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">dateblocage</text>""" % {'x': -38+x, 'y': 76.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity UtilisateurBlocage -->"""
(x,y) = (cx[u"UtilisateurBlocage"],cy[u"UtilisateurBlocage"])
lines += u"""\n<g id="entity-UtilisateurBlocage">""" % {}
lines += u"""\n	<g id="frame-UtilisateurBlocage">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="120" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -60+x, 'y': -34+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="120" height="43" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -60+x, 'y': -9.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="120" height="68" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -60+x, 'y': -34+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -60+x, 'y0': -9+y, 'x1': 60+x, 'y1': -9+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">UtilisateurBlocage</text>""" % {'x': -55+x, 'y': -16.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">bloqueur</text>""" % {'x': -55+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -55+x, 'y0': 11+y, 'x1': -1+x, 'y1': 11+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">blocage</text>""" % {'x': -55+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -55+x, 'y0': 28+y, 'x1': -8+x, 'y1': 28+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity Couple -->"""
(x,y) = (cx[u"Couple"],cy[u"Couple"])
lines += u"""\n<g id="entity-Couple">""" % {}
lines += u"""\n	<g id="frame-Couple">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="74" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -37+x, 'y': -51+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="74" height="77" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -37+x, 'y': -26.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="74" height="102" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -37+x, 'y': -51+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -37+x, 'y0': -26+y, 'x1': 37+x, 'y1': -26+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">Couple</text>""" % {'x': -21+x, 'y': -33.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">id</text>""" % {'x': -32+x, 'y': -8.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -32+x, 'y0': -6+y, 'x1': -20+x, 'y1': -6+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">partenaire</text>""" % {'x': -32+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -32+x, 'y0': 11+y, 'x1': 31+x, 'y1': 11+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">datedebut</text>""" % {'x': -32+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -32+x, 'y0': 28+y, 'x1': 30+x, 'y1': 28+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">datefin</text>""" % {'x': -32+x, 'y': 42.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity Personne -->"""
(x,y) = (cx[u"Personne"],cy[u"Personne"])
lines += u"""\n<g id="entity-Personne">""" % {}
lines += u"""\n	<g id="frame-Personne">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="112" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -56+x, 'y': -102+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="112" height="179" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -56+x, 'y': -77.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="112" height="204" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -56+x, 'y': -102+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -56+x, 'y0': -77+y, 'x1': 56+x, 'y1': -77+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">Personne</text>""" % {'x': -28+x, 'y': -84.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">id</text>""" % {'x': -51+x, 'y': -59.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -51+x, 'y0': -57+y, 'x1': -39+x, 'y1': -57+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nom</text>""" % {'x': -51+x, 'y': -42.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">prenom</text>""" % {'x': -51+x, 'y': -25.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">homme</text>""" % {'x': -51+x, 'y': -8.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">datedenaissance</text>""" % {'x': -51+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">datededeces</text>""" % {'x': -51+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">arbre</text>""" % {'x': -51+x, 'y': 42.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">dateajout</text>""" % {'x': -51+x, 'y': 59.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">pere</text>""" % {'x': -51+x, 'y': 76.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">mere</text>""" % {'x': -51+x, 'y': 93.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity Role -->"""
(x,y) = (cx[u"Role"],cy[u"Role"])
lines += u"""\n<g id="entity-Role">""" % {}
lines += u"""\n	<g id="frame-Role">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="38" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -19+x, 'y': -34+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="38" height="43" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -19+x, 'y': -9.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="38" height="68" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -19+x, 'y': -34+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -19+x, 'y0': -9+y, 'x1': 19+x, 'y1': -9+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">Role</text>""" % {'x': -14+x, 'y': -16.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">id</text>""" % {'x': -14+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -14+x, 'y0': 11+y, 'x1': -2+x, 'y1': 11+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nom</text>""" % {'x': -14+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity UtilisateurArbreLit -->"""
(x,y) = (cx[u"UtilisateurArbreLit"],cy[u"UtilisateurArbreLit"])
lines += u"""\n<g id="entity-UtilisateurArbreLit">""" % {}
lines += u"""\n	<g id="frame-UtilisateurArbreLit">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="120" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -60+x, 'y': -42+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="120" height="59" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -60+x, 'y': -17.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="120" height="84" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -60+x, 'y': -42+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -60+x, 'y0': -17+y, 'x1': 60+x, 'y1': -17+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">UtilisateurArbreLit</text>""" % {'x': -55+x, 'y': -24.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">lecteur</text>""" % {'x': -55+x, 'y': 0.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -55+x, 'y0': 3+y, 'x1': -13+x, 'y1': 3+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">arbre</text>""" % {'x': -55+x, 'y': 17.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -55+x, 'y0': 20+y, 'x1': -22+x, 'y1': 20+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">datelecture</text>""" % {'x': -55+x, 'y': 34.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -55+x, 'y0': 37+y, 'x1': 14+x, 'y1': 37+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity UtilisateurRole -->"""
(x,y) = (cx[u"UtilisateurRole"],cy[u"UtilisateurRole"])
lines += u"""\n<g id="entity-UtilisateurRole">""" % {}
lines += u"""\n	<g id="frame-UtilisateurRole">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="98" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -49+x, 'y': -34+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="98" height="43" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -49+x, 'y': -9.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="98" height="68" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -49+x, 'y': -34+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -49+x, 'y0': -9+y, 'x1': 49+x, 'y1': -9+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">UtilisateurRole</text>""" % {'x': -44+x, 'y': -16.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">utilisateur</text>""" % {'x': -44+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -44+x, 'y0': 11+y, 'x1': 17+x, 'y1': 11+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">role</text>""" % {'x': -44+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -44+x, 'y0': 28+y, 'x1': -20+x, 'y1': 28+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity Abonnement -->"""
(x,y) = (cx[u"Abonnement"],cy[u"Abonnement"])
lines += u"""\n<g id="entity-Abonnement">""" % {}
lines += u"""\n	<g id="frame-Abonnement">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="146" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -73+x, 'y': -85+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="146" height="145" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -73+x, 'y': -60.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="146" height="170" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -73+x, 'y': -85+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -73+x, 'y0': -60+y, 'x1': 73+x, 'y1': -60+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">Abonnement</text>""" % {'x': -39+x, 'y': -67.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">id</text>""" % {'x': -68+x, 'y': -42.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -68+x, 'y0': -40+y, 'x1': -56+x, 'y1': -40+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nom</text>""" % {'x': -68+x, 'y': -25.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">description</text>""" % {'x': -68+x, 'y': -8.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">duree</text>""" % {'x': -68+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">prix</text>""" % {'x': -68+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">actif</text>""" % {'x': -68+x, 'y': 42.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nombremaxarbres</text>""" % {'x': -68+x, 'y': 59.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nombremaxpersonnes</text>""" % {'x': -68+x, 'y': 76.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity UtilisateurAbonnement -->"""
(x,y) = (cx[u"UtilisateurAbonnement"],cy[u"UtilisateurAbonnement"])
lines += u"""\n<g id="entity-UtilisateurAbonnement">""" % {}
lines += u"""\n	<g id="frame-UtilisateurAbonnement">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="150" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -75+x, 'y': -51+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="150" height="77" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -75+x, 'y': -26.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="150" height="102" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -75+x, 'y': -51+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -75+x, 'y0': -26+y, 'x1': 75+x, 'y1': -26+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">UtilisateurAbonnement</text>""" % {'x': -70+x, 'y': -33.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">abonne</text>""" % {'x': -70+x, 'y': -8.3+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -70+x, 'y0': -6+y, 'x1': -25+x, 'y1': -6+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">abonnement</text>""" % {'x': -70+x, 'y': 8.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -70+x, 'y0': 11+y, 'x1': 7+x, 'y1': 11+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">date</text>""" % {'x': -70+x, 'y': 25.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -70+x, 'y0': 28+y, 'x1': -42+x, 'y1': 28+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">cartedepayement</text>""" % {'x': -70+x, 'y': 42.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Entity Blocage -->"""
(x,y) = (cx[u"Blocage"],cy[u"Blocage"])
lines += u"""\n<g id="entity-Blocage">""" % {}
lines += u"""\n	<g id="frame-Blocage">""" % {}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="78" height="25" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -39+x, 'y': -42+y, 'color': colors['entity_cartouche_color'], 'stroke_color': colors['entity_cartouche_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="78" height="59" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="0"/>""" % {'x': -39+x, 'y': -17.0+y, 'color': colors['entity_color'], 'stroke_color': colors['entity_color']}
lines += u"""\n		<rect x="%(x)s" y="%(y)s" width="78" height="84" fill="%(color)s" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x': -39+x, 'y': -42+y, 'color': colors['transparent_color'], 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n		<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -39+x, 'y0': -17+y, 'x1': 39+x, 'y1': -17+y, 'stroke_color': colors['entity_stroke_color']}
lines += u"""\n	</g>""" % {}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">Blocage</text>""" % {'x': -24+x, 'y': -24.3+y, 'text_color': colors['entity_cartouche_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">id</text>""" % {'x': -34+x, 'y': 0.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<line x1="%(x0)s" y1="%(y0)s" x2="%(x1)s" y2="%(y1)s" stroke="%(stroke_color)s" stroke-width="1"/>""" % {'x0': -34+x, 'y0': 3+y, 'x1': -22+x, 'y1': 3+y, 'stroke_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">nom</text>""" % {'x': -34+x, 'y': 17.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n	<text x="%(x)s" y="%(y)s" fill="%(text_color)s" font-family="Verdana" font-size="12">description</text>""" % {'x': -34+x, 'y': 34.8+y, 'text_color': colors['entity_attribute_text_color']}
lines += u"""\n</g>""" % {}

lines += u"""\n\n<!-- Link from "id" (Arbre) to "id" (Utilisateur) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"Arbre"]+43*fs_ps[0] - cx[u"Utilisateur"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Arbre"]+43*fs,cy[u"Arbre"]+4.5,cx[u"Utilisateur"]+56*ps,cy[u"Utilisateur"]+-80.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "blocage" (Arbre) to "blocage" (UtilisateurBlocage) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"Arbre"]+43*fs_ps[0] - cx[u"UtilisateurBlocage"]-60*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Arbre"]+43*fs,cy[u"Arbre"]+38.5,cx[u"UtilisateurBlocage"]+60*ps,cy[u"UtilisateurBlocage"]+21.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "bloqueur" (Arbre) to "bloqueur" (UtilisateurBlocage) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"Arbre"]+43*fs_ps[0] - cx[u"UtilisateurBlocage"]-60*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Arbre"]+43*fs,cy[u"Arbre"]+55.5,cx[u"UtilisateurBlocage"]+60*ps,cy[u"UtilisateurBlocage"]+4.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurBlocage) to "id" (Utilisateur) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"UtilisateurBlocage"]+60*fs_ps[0] - cx[u"Utilisateur"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurBlocage"]+60*fs,cy[u"UtilisateurBlocage"]+4.5,cx[u"Utilisateur"]+56*ps,cy[u"Utilisateur"]+-80.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurBlocage) to "id" (Blocage) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"UtilisateurBlocage"]+60*fs_ps[0] - cx[u"Blocage"]-39*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurBlocage"]+60*fs,cy[u"UtilisateurBlocage"]+21.5,cx[u"Blocage"]+39*ps,cy[u"Blocage"]+-3.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (Couple) to "id" (Personne) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"Couple"]+37*fs_ps[0] - cx[u"Personne"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Couple"]+37*fs,cy[u"Couple"]+-12.5,cx[u"Personne"]+56*ps,cy[u"Personne"]+-63.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (Couple) to "id" (Personne) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"Couple"]+37*fs_ps[0] - cx[u"Personne"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Couple"]+37*fs,cy[u"Couple"]+4.5,cx[u"Personne"]+56*ps,cy[u"Personne"]+-63.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (Personne) to "id" (Arbre) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"Personne"]+56*fs_ps[0] - cx[u"Arbre"]-43*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Personne"]+56*fs,cy[u"Personne"]+38.5,cx[u"Arbre"]+43*ps,cy[u"Arbre"]+-46.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (Personne) to "id" (Personne) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"Personne"]+56*fs_ps[0] - cx[u"Personne"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Personne"]+56*fs,cy[u"Personne"]+72.5,cx[u"Personne"]+56*ps,cy[u"Personne"]+-63.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (Personne) to "id" (Personne) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"Personne"]+56*fs_ps[0] - cx[u"Personne"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"Personne"]+56*fs,cy[u"Personne"]+89.5,cx[u"Personne"]+56*ps,cy[u"Personne"]+-63.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurArbreLit) to "id" (Utilisateur) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"UtilisateurArbreLit"]+60*fs_ps[0] - cx[u"Utilisateur"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurArbreLit"]+60*fs,cy[u"UtilisateurArbreLit"]+-3.5,cx[u"Utilisateur"]+56*ps,cy[u"Utilisateur"]+-80.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurArbreLit) to "id" (Arbre) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"UtilisateurArbreLit"]+60*fs_ps[0] - cx[u"Arbre"]-43*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurArbreLit"]+60*fs,cy[u"UtilisateurArbreLit"]+13.5,cx[u"Arbre"]+43*ps,cy[u"Arbre"]+-46.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurRole) to "id" (Utilisateur) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"UtilisateurRole"]+49*fs_ps[0] - cx[u"Utilisateur"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurRole"]+49*fs,cy[u"UtilisateurRole"]+4.5,cx[u"Utilisateur"]+56*ps,cy[u"Utilisateur"]+-80.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurRole) to "id" (Role) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"UtilisateurRole"]+49*fs_ps[0] - cx[u"Role"]-19*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurRole"]+49*fs,cy[u"UtilisateurRole"]+21.5,cx[u"Role"]+19*ps,cy[u"Role"]+4.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurAbonnement) to "id" (Utilisateur) -->"""
(fs,ps) = (min([(1, 1), (-1, 1), (1, -1), (-1, -1)], key=lambda fs_ps: abs(cx[u"UtilisateurAbonnement"]+75*fs_ps[0] - cx[u"Utilisateur"]-56*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurAbonnement"]+75*fs,cy[u"UtilisateurAbonnement"]+-12.5,cx[u"Utilisateur"]+56*ps,cy[u"Utilisateur"]+-80.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}

lines += u"""\n\n<!-- Link from "id" (UtilisateurAbonnement) to "id" (Abonnement) -->"""
(fs,ps) = (min([(-1, -1), (1, -1), (-1, 1), (1, 1)], key=lambda fs_ps: abs(cx[u"UtilisateurAbonnement"]+75*fs_ps[0] - cx[u"Abonnement"]-73*fs_ps[1])))
(xf,yf,xp,yp) = (cx[u"UtilisateurAbonnement"]+75*fs,cy[u"UtilisateurAbonnement"]+4.5,cx[u"Abonnement"]+73*ps,cy[u"Abonnement"]+-46.5)
lines += u"""\n<path d="M%(x0)s %(y0)s C %(x1)s %(y1)s %(x2)s %(y2)s %(x3)s %(y3)s" fill="none" stroke="%(stroke_color)s" stroke-width="2"/>""" % {'x0': xf, 'y0': yf, 'x1': xf+(xp-xf)/2 if fs != ps else xf+54*fs, 'y1': yf+(yp-yf)/2, 'x2': xf+(xp-xf)/3 if fs != ps else xp+54*ps, 'y2': yp, 'x3': xp, 'y3': yp, 'stroke_color': colors['leg_stroke_color']}
path = arrow(xp,yp,ps,0)
lines += u"""\n<path d="%(path)s" fill="%(stroke_color)s" stroke-width="0"/>""" % {'path': path, 'stroke_color': colors['leg_stroke_color']}
lines += u"""\n<circle cx="%(cx)s" cy="%(cy)s" r="2" stroke="%(stroke_color)s" stroke-width="2" fill="%(color)s"/>""" % {'cx': xf, 'cy': yf, 'stroke_color': colors['leg_stroke_color'], 'color': colors['leg_stroke_color']}
lines += u'\n</svg>'

with codecs.open("DBGenialogie_toutsaufblabla.svg", "w", "utf8") as f:
    f.write(lines)
safe_print_for_PHP(u'Fichier de sortie "DBGenialogie_toutsaufblabla.svg" généré avec succès.')