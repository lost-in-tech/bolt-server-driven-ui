/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    // Or if using `src` directory:
    "./src/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    container: {
      center: true,
      padding: "2rem",
      screens: {
        "2xl": "1400px",
      },
    },
    extend: {
      colors: {
        border: "hsl(var(--border))",
        input: "hsl(var(--input))",
        ring: "hsl(var(--ring))",
        background: "hsl(var(--background))",
        foreground: "hsl(var(--foreground))",
        destructive: {
          DEFAULT: "hsl(var(--destructive))",
          foreground: "hsl(var(--destructive-foreground))",
        },
        muted: {
          DEFAULT: "hsl(var(--muted))",
          foreground: "hsl(var(--muted-foreground))",
        },
        popover: {
          DEFAULT: "hsl(var(--popover))",
          foreground: "hsl(var(--popover-foreground))",
        },
        card: {
          DEFAULT: "hsl(var(--card))",
          foreground: "hsl(var(--card-foreground))",
        },

        primary: "oklch(var(--primary))",
        "primary-content": "oklch(var(--primary-content))",
        secondary: "oklch(var(--secondary))",
        "secondary-content": "oklch(var(--secondary-content))",
        accent: "oklch(var(--accent))",
        "accent-content": "oklch(var(--accent-content))",
        neutral: "oklch(var(--neutral))",
        "neutral-content": "oklch(var(--neutral-content))",
        "base-100": "oklch(var(--base-100))",
        "base-200": "oklch(var(--base-200))",
        "base-300": "oklch(var(--base-300))",
        "base-content": "oklch(var(--base-content))",
        info: "oklch(var(--info))",
        "info-content": "oklch(var(--info-content))",
        success: "oklch(var(--success))",
        "success-content": "oklch(var(--success-content))",
        warning: "oklch(var(--warning))",
        "warning-content": "oklch(var(--warning-content))",
        error: "oklch(var(--error))",
        "error-content": "oklch(var(--error-content))",
      },
      padding: {
        none: "var(--space-none)",
        xs: "var(--space-xs)",
        sm: "var(--space-sm)",
        md: "var(--space-md)",
        lg: "var(--space-lg)",
        xl: "var(--space-xl)",
      },
      margin: {
        none: "var(--space-none)",
        xs: "var(--space-xs)",
        sm: "var(--space-sm)",
        md: "var(--space-md)",
        lg: "var(--space-lg)",
        xl: "var(--space-xl)",
      },
      borderRadius: {
        lg: "var(--radius)",
        md: "calc(var(--radius) - 2px)",
        sm: "calc(var(--radius) - 4px)",
      },
      borderWidth: {
        none: "var(--border-width-none)",
        regular: "var(--border-width-regular)",
        medium: "var(--border-width-medium)",
        thick: "var(--border-width-thick)",
        "extra-thick": "var(--border-width-extra-thick)",
      },
      keyframes: {
        "accordion-down": {
          from: { height: "0" },
          to: { height: "var(--radix-accordion-content-height)" },
        },
        "accordion-up": {
          from: { height: "var(--radix-accordion-content-height)" },
          to: { height: "0" },
        },
      },
      animation: {
        "accordion-down": "accordion-down 0.2s ease-out",
        "accordion-up": "accordion-up 0.2s ease-out",
      },
    },
  },
  plugins: [],
};
